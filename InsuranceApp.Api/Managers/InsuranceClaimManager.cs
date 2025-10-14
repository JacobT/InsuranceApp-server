using AutoMapper;
using InsuranceApp.Api.Managers.Interfaces;
using InsuranceApp.Api.Models;
using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Managers;

/// <summary>
/// Provides application-layer management of <see cref="InsuranceClaim"/> entities, 
/// including CRUD operations and retrieval of claims related to specific insurance.
/// </summary>
public class InsuranceClaimManager : BaseManager<InsuranceClaim, InsuranceClaimDTO>, IInsuranceClaimManager
{
    private readonly IRelatedEntityService<Insurance, InsuranceClaim, IInsuranceRepository, IInsuranceClaimRepository> _entityService;

    public InsuranceClaimManager(
        IInsuranceClaimRepository repository,
        IMapper mapper,
        IRelatedEntityService<Insurance, InsuranceClaim, IInsuranceRepository, IInsuranceClaimRepository> entityService)
        : base(repository, mapper)
    {
        _entityService = entityService;
    }

    /// <inheritdoc />
    /// <remarks>
    /// Uses <see cref="IRelatedEntityService{TParent,TChild,TParentRepository,TChildRepository}"/> 
    /// to retrieve all claims for a given insurance.
    /// </remarks>
    public async Task<IList<InsuranceClaimDTO>?> GetByParentId(int insuranceId) =>
        _mapper.Map<IList<InsuranceClaimDTO>>(await _entityService.GetByParentId(insuranceId));
}
