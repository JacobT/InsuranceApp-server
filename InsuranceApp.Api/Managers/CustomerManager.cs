using AutoMapper;
using InsuranceApp.Api.Managers.Interfaces;
using InsuranceApp.Api.Models;
using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Managers;

/// <summary>
/// Provides application-layer management of <see cref="Customer"/> entities, 
/// including CRUD operations and retrieval of detailed customer information.
/// </summary>
public class CustomerManager : BaseManager<Customer, CustomerDTO>, ICustomerManager
{
    private readonly IDetailService<Customer, ICustomerRepository> _detailService;

    public CustomerManager(
        ICustomerRepository repository,
        IMapper mapper,
        IDetailService<Customer, ICustomerRepository> detailService)
        : base(repository, mapper)
    {
        _detailService = detailService;
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation retrieves the <see cref="Customer"/> entity 
    /// using <see cref="IDetailService{TEntity, TRepository}"/>, 
    /// and maps it to a <see cref="CustomerDetailDTO"/>.  
    /// If the customer does not exist, <c>null</c> is returned.
    /// </remarks>
    public async Task<CustomerDetailDTO?> GetDetailAsync(int customerId) =>
        _mapper.Map<CustomerDetailDTO>(await _detailService.GetDetailAsync(customerId));
}
