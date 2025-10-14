using InsuranceApp.Api.Infrastructure;
using InsuranceApp.Api.Managers.Interfaces;
using InsuranceApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.Api.Controllers;

/// <summary>
/// Provides API endpoints for managing insurance claims.
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ClaimsController : ControllerBase
{
    private readonly IInsuranceClaimManager _manager;

    public ClaimsController(IInsuranceClaimManager manager) => _manager = manager;

    /// <summary>
    /// Retrieves all claims or claims for a specific insurance.
    /// </summary>
    /// <param name="insuranceId">Optional. The ID of the parent insurance to filter claims.</param>
    /// <returns>A list of <see cref="InsuranceClaimDTO"/>.</returns>
    /// <response code="200">Returns the list of claims (empty if none found).</response>
    [HttpGet]
    [HttpGet("~/api/insurances/{insuranceId:int}/[controller]")]
    public async Task<ActionResult<IEnumerable<InsuranceClaimDTO>>> GetAll(int? insuranceId)
    {
        IList<InsuranceClaimDTO>? claims;
        if (insuranceId is not null)
        {
            claims = await _manager.GetByParentId(insuranceId.Value);
            if (claims is null)
            {
                return Ok(new List<InsuranceClaimDTO>());
            }
        }
        else
        {
            claims = await _manager.GetAllAsync();
        }
        return Ok(claims);
    }

    /// <summary>
    /// Retrieves a specific claim by ID.
    /// </summary>
    /// <param name="claimId">The ID of the claim.</param>
    /// <returns>The <see cref="InsuranceClaimDTO"/> with the specified ID.</returns>
    /// <response code="200">Returns the claim.</response>
    /// <response code="404">If the claim is not found.</response>
    [HttpGet("{claimId:int}")]
    public async Task<ActionResult<InsuranceClaimDTO>> FindById(int claimId)
    {
        InsuranceClaimDTO? claim = await _manager.FindByIdAsync(claimId);
        if (claim is null)
        {
            return NotFound();
        }

        return Ok(claim);
    }

    /// <summary>
    /// Creates a new insurance claim.
    /// </summary>
    /// <param name="claimDTO">The claim data to create.</param>
    /// <returns>The created <see cref="InsuranceClaimDTO"/>.</returns>
    /// <response code="201">Returns the created claim.</response>
    [HttpPost]
    public async Task<ActionResult<InsuranceClaimDTO>> Create([FromBody] InsuranceClaimDTO claimDTO)
    {
        InsuranceClaimDTO newClaim = await _manager.InsertAsync(claimDTO);
        return CreatedAtAction(nameof(FindById), new { claimId = newClaim.Id}, newClaim);
    }

    /// <summary>
    /// Updates an existing insurance claim.
    /// </summary>
    /// <param name="claimId">The ID of the claim to update.</param>
    /// <param name="claimDTO">The updated claim data.</param>
    /// <returns>The updated <see cref="InsuranceClaimDTO"/>.</returns>
    /// <response code="200">Returns the updated claim.</response>
    /// <response code="404">If the claim does not exist.</response>
    [Authorize(Policy = Policies.AdminOrManager)]
    [HttpPut("{claimId:int}")]
    public async Task<ActionResult<InsuranceClaimDTO>> Update(int claimId, [FromBody] InsuranceClaimDTO claimDTO)
    {
        if (claimId != claimDTO.Id)
        {
            return BadRequest("Route ID and body ID must match.");
        }

            InsuranceClaimDTO? updatedClaim = await _manager.UpdateAsync(claimDTO);
        if (updatedClaim is null)
        {
            return NotFound();
        }

        return Ok(updatedClaim);
    }

    /// <summary>
    /// Deletes a claim by ID.
    /// </summary>
    /// <param name="claimId">The ID of the claim to delete.</param>
    /// <returns>The deleted <see cref="InsuranceClaimDTO"/>.</returns>
    /// <response code="200">Returns the deleted claim.</response>
    /// <response code="404">If the claim does not exist.</response>
    [Authorize(Policy = Policies.AdminOrManager)]
    [HttpDelete("{claimId:int}")]
    public async Task<ActionResult<InsuranceClaimDTO>> Delete(int claimId)
    {
        InsuranceClaimDTO? deletedClaim = await _manager.DeleteAsync(claimId);
        if (deletedClaim is null)
        {
            return NotFound();
        }

        return Ok(deletedClaim);
    }
}
