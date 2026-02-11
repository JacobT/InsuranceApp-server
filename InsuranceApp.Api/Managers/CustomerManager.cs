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
    private readonly IFilterService<Customer, ICustomerRepository> _filterService;

    public CustomerManager(
        ICustomerRepository repository,
        IMapper mapper,
        IDetailService<Customer, ICustomerRepository> detailService,
        IFilterService<Customer, ICustomerRepository> filterService)
        : base(repository, mapper)
    {
        _detailService = detailService;
        _filterService = filterService;
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation retrieves a list of <see cref="Customer"/> entities 
    /// using <see cref="IFilterService{TEntity, TRepository}"/>, 
    /// and maps it to a list of <see cref="CustomerDTO"/>.
    /// </remarks>
    public async Task<IList<CustomerDTO>> GetAllAsync(string search) =>
        _mapper.Map<IList<CustomerDTO>>(await _filterService.GetAllAsync(search));

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
