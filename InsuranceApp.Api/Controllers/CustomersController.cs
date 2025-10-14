using InsuranceApp.Api.Infrastructure;
using InsuranceApp.Api.Managers.Interfaces;
using InsuranceApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.Api.Controllers;

/// <summary>
/// Provides API endpoints for managing customers.
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerManager _customerManager;

    public CustomersController(ICustomerManager customerManager) => _customerManager = customerManager;

    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    /// <returns>A list of <see cref="CustomerDTO"/> representing all customers.</returns>
    /// <response code="200">Returns the list of customers (empty if none found).</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll()
    {
        IList<CustomerDTO> customerDTOs = await _customerManager.GetAllAsync();
        return Ok(customerDTOs);
    }

    /// <summary>
    /// Retrieves detailed information about a specific customer by ID.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The <see cref="CustomerDetailDTO"/> of the specified customer, or NotFound if not found.</returns>
    /// <response code="200">Returns the customer.</response>
    /// <response code="404">If the customer is not found.</response>
    [HttpGet("{customerId:int}")]
    public async Task<ActionResult<CustomerDetailDTO>> GetDetail(int customerId)
    {
        CustomerDetailDTO? customer = await _customerManager.GetDetailAsync(customerId);
        if (customer is null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerDTO">The customer data to create.</param>
    /// <returns>The created <see cref="CustomerDTO"/> with a 201 Created status.</returns>
    /// <response code="201">Returns the created customer.</response>
    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> Create([FromBody]CustomerDTO customerDTO)
    {
        CustomerDTO newCustomer = await _customerManager.InsertAsync(customerDTO);
        return CreatedAtAction(nameof(GetDetail), new { customerId = newCustomer.Id }, newCustomer);
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="customerId">The ID of the customer to update.</param>
    /// <param name="customerDTO">The updated customer data.</param>
    /// <returns>The updated <see cref="CustomerDTO"/>, or NotFound if the customer does not exist.</returns>
    /// <response code="200">Returns the updated customer.</response>
    /// <response code="404">If the customer does not exist.</response>
    [Authorize(Policy = Policies.AdminOrManager)]
    [HttpPut("{customerId:int}")]
    public async Task<ActionResult<CustomerDTO>> Update(int customerId, [FromBody]CustomerDTO customerDTO)
    {
        if (customerId != customerDTO.Id)
        {
            return BadRequest("Route ID and body ID must match.");
        }

        CustomerDTO? updatedCustomer = await _customerManager.UpdateAsync(customerDTO);
        if (updatedCustomer is null)
        {
            return NotFound();
        }

        return Ok(updatedCustomer);
    }

    /// <summary>
    /// Deletes a customer by ID.
    /// </summary>
    /// <param name="customerId">The ID of the customer to delete.</param>
    /// <returns>The deleted <see cref="CustomerDTO"/> or NotFound if the customer does not exist.</returns>
    /// <response code="200">Returns the deleted customer.</response>
    /// <response code="404">If the customer does not exist.</response>
    [Authorize(Policy = Policies.AdminOrManager)]
    [HttpDelete("{customerId:int}")]
    public async Task<ActionResult<CustomerDTO?>> Delete(int customerId)
    {
        CustomerDTO? deletedCustomer = await _customerManager.DeleteAsync(customerId);
        if (deletedCustomer is null)
        {
            return NotFound();
        }

        return Ok(deletedCustomer);
    }
}
