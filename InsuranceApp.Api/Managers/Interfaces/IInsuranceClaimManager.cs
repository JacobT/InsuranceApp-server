using InsuranceApp.Api.Models;
using InsuranceApp.Data.Models;

namespace InsuranceApp.Api.Managers.Interfaces;

/// <summary>
/// Defines application-layer operations for managing <see cref="InsuranceClaim"/> entities 
/// and mapping them to <see cref="InsuranceClaimDTO"/> objects.
/// </summary>
public interface IInsuranceClaimManager : IBaseManager<InsuranceClaim, InsuranceClaimDTO>
{
    /// <summary>
    /// Retrieves all <see cref="InsuranceClaimDTO"/> associated with a specific insurance.
    /// </summary>
    /// <param name="insuranceId">The unique identifier of the insurance.</param>
    /// <returns>
    ///A task that represents the asynchronous operation.
    ///The task result contains a list of <see cref="InsuranceClaimDTO"/> for the specified insurance; 
    /// returns <c>null</c> if the insurance does not exist.
    /// </returns>
    Task<IList<InsuranceClaimDTO>?> GetByParentId(int insuranceId);
}
