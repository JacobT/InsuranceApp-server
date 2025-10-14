using InsuranceApp.Api.Infrastructure;
using InsuranceApp.Api.Managers.Interfaces;
using InsuranceApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.Api.Controllers;

/// <summary>
/// Provides API endpoints for managing insurances.
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class InsurancesController : ControllerBase
{
    private readonly IInsuranceManager _insuranceManager;

    public InsurancesController(IInsuranceManager insuranceManager) => _insuranceManager = insuranceManager;

    /// <summary>
    /// Retrieves all insurances or insurances for a specific customer.
    /// </summary>
    /// <param name="customerId">Optional. The ID of the parent customer to filter insurances.</param>
    /// <returns>A list of <see cref="InsuranceDTO"/>.</returns>
    /// /// <response code="200">Returns the list of insurances (empty if none found).</response>
    [HttpGet]
    [HttpGet("~/api/customers/{customerId:int}/[controller]")]
    public async Task<ActionResult<IEnumerable<InsuranceDTO>>> GetAll(int? customerId)
    {
        IList<InsuranceDTO>? insuranceDTOs;
        if (customerId is not null)
        {
            insuranceDTOs = await _insuranceManager.GetByParentIdAsync(customerId.Value);
            if (insuranceDTOs is null)
            {
                return Ok(new List<InsuranceDTO>());
            }
        }
        else
        {
            insuranceDTOs = await _insuranceManager.GetAllAsync();
        }

        return Ok(insuranceDTOs);
    }

    /// <summary>
    /// Retrieves detailed information about a specific insurance by ID.
    /// </summary>
    /// <param name="insuranceId">The ID of the insurance.</param>
    /// <returns>The <see cref="InsuranceDetailDTO"/> of the specified insurance, or NotFound if not found.</returns>
    /// <response code="200">Returns the insurance.</response>
    /// <response code="404">If the insurance is not found.</response>
    [HttpGet("{insuranceId:int}")]
    public async Task<ActionResult<InsuranceDTO>> GetDetail(int insuranceId)
    {
        InsuranceDetailDTO? insurance = await _insuranceManager.GetDetailAsync(insuranceId);
        if (insurance is null)
        {
            return NotFound();
        }

        return Ok(insurance);
    }

    /// <summary>
    /// Creates a new insurance.
    /// </summary>
    /// <param name="insuranceDTO">The insurance data to create.</param>
    /// <returns>The created <see cref="InsuranceDTO"/> with a 201 Created status.</returns>
    /// <response code="201">Returns the created insurance.</response>
    [HttpPost]
    public async Task<ActionResult<InsuranceDTO>> Create([FromBody] InsuranceDTO insuranceDTO)
    {
        InsuranceDTO newInsurance = await _insuranceManager.InsertAsync(insuranceDTO);
        return CreatedAtAction(nameof(GetDetail), new { insuranceId = newInsurance.Id }, newInsurance);
    }

    /// <summary>
    /// Updates an existing insurance.
    /// </summary>
    /// <param name="insuranceId">The ID of the insurance to update.</param>
    /// <param name="insuranceDTO">The updated insurance data.</param>
    /// <returns>The updated <see cref="InsuranceDTO"/>, or NotFound if the insurance does not exist.</returns>
    /// <response code="200">Returns the updated insurance.</response>
    /// <response code="404">If the insurance does not exist.</response>
    [Authorize(Policy = Policies.AdminOrManager)]
    [HttpPut("{insuranceId:int}")]
    public async Task<ActionResult<InsuranceDTO>> Update(int insuranceId, [FromBody] InsuranceDTO insuranceDTO)
    {
        if (insuranceId != insuranceDTO.Id)
        {
            return BadRequest("Route ID and body ID must match.");
        }

        InsuranceDTO? updatedInsurance = await _insuranceManager.UpdateAsync(insuranceDTO);
        if (updatedInsurance is null)
        {
            return NotFound();
        }

        return Ok(updatedInsurance);
    }

    /// <summary>
    /// Deletes an insurance by ID.
    /// </summary>
    /// <param name="insuranceId">The ID of the insurance to delete.</param>
    /// <returns>The deleted <see cref="InsuranceDTO"/> or NotFound if the insurance does not exist.</returns>
    /// <response code="200">Returns the deleted insurance.</response>
    /// <response code="404">If the insurance does not exist.</response>
    [Authorize(Policy = Policies.AdminOrManager)]
    [HttpDelete("{insuranceId:int}")]
    public async Task<ActionResult<InsuranceDTO>> Delete(int insuranceId)
    {
        InsuranceDTO? deletedInsurance = await _insuranceManager.DeleteAsync(insuranceId);
        if (deletedInsurance is null)
        {
            return NotFound();
        }

        return Ok(deletedInsurance);
    }
}
