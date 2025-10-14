using InsuranceApp.Api.Models;
using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Models;

namespace InsuranceApp.Api.Managers.Interfaces;

/// <summary>
/// Defines application-layer operations for managing <see cref="Customer"/> entities 
/// and mapping them to <see cref="CustomerDTO"/> objects.
/// </summary>
public interface ICustomerManager : IBaseManager<Customer, CustomerDTO>
{
    /// <summary>
    /// Retrieves detailed information about a single <see cref="Customer"/> entity.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains a <see cref="CustomerDetailDTO"/> if the customer exists; otherwise, <c>null</c>.
    /// </returns>
    Task<CustomerDetailDTO?> GetDetailAsync(int customerId);
}
