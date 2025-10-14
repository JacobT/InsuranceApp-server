using AutoMapper;
using InsuranceApp.Api.Managers.Interfaces;
using InsuranceApp.Api.Models;
using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Managers;

/// <summary>
/// Provides application-layer management of <see cref="Insurance"/> entities, 
/// including CRUD operations, retrieval of detailed insurance information
/// and retrieval of insurances related to specific customer.
/// </summary>
public class InsuranceManager : BaseManager<Insurance, InsuranceDTO>, IInsuranceManager
{
    private readonly IDetailService<Insurance, IInsuranceRepository> _detailService;
    private readonly IRelatedEntityService<Customer, Insurance, ICustomerRepository, IInsuranceRepository> _relatedEntityService;

    public InsuranceManager(
        IInsuranceRepository repository,
        IMapper mapper,
        IDetailService<Insurance, IInsuranceRepository> detailService,
        IRelatedEntityService<Customer, Insurance, ICustomerRepository, IInsuranceRepository> insuranceRelatedEntityService)
        : base(repository, mapper)
    {
        _detailService = detailService;
        _relatedEntityService = insuranceRelatedEntityService;
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation retrieves the <see cref="Insurance"/> entity 
    /// using <see cref="IDetailService{TEntity, TRepository}"/>, 
    /// and maps it to a <see cref="InsuranceDetailDTO"/>.  
    /// If the insurance does not exist, <c>null</c> is returned.
    /// </remarks>
    public async Task<InsuranceDetailDTO?> GetDetailAsync(int insuranceId) =>
        _mapper.Map<InsuranceDetailDTO>(await _detailService.GetDetailAsync(insuranceId));

    /// <inheritdoc />
    /// <remarks>
    /// Uses <see cref="IRelatedEntityService{TParent,TChild,TParentRepository,TChildRepository}"/> 
    /// to retrieve all insurances for a given customer.
    /// </remarks>
    public async Task<IList<InsuranceDTO>?> GetByParentIdAsync(int customerId) =>
        _mapper.Map<IList<InsuranceDTO>>(await _relatedEntityService.GetByParentId(customerId));
}
