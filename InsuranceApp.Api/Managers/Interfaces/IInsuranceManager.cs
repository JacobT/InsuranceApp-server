using InsuranceApp.Api.Models;
using InsuranceApp.Data.Models;

namespace InsuranceApp.Api.Managers.Interfaces;

/// <summary>
/// Defines application-layer operations for managing <see cref="Insurance"/> entities 
/// and mapping them to <see cref="InsuranceDTO"/> objects.
/// </summary>
public interface IInsuranceManager : IBaseManager<Insurance, InsuranceDTO>
{
    /// <summary>
    /// Retrieves all <see cref="InsuranceDTO"/> associated with a specific customer.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>
    ///A task that represents the asynchronous operation.
    ///The task result contains a list of <see cref="InsuranceDTO"/> for the specified customer; 
    /// returns <c>null</c> if the customer does not exist.
    /// </returns>
    Task<IList<InsuranceDTO>?> GetByParentIdAsync(int customerId);

    /// <summary>
    /// Retrieves detailed information about a single <see cref="Insurance"/> entity.
    /// </summary>
    /// <param name="insuranceId">The unique identifier of the insurance.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains a <see cref="InsuranceDetailDTO"/> if the insurance exists; otherwise, <c>null</c>.
    /// </returns>
    Task<InsuranceDetailDTO?> GetDetailAsync(int insuranceId);
}
