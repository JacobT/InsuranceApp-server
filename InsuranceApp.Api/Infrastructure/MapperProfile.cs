using AutoMapper;
using InsuranceApp.Api.Models;
using InsuranceApp.Data.Models;

namespace InsuranceApp.Api.Infrastructure;

/// <summary>
/// Defines AutoMapper configuration for mapping between entity and DTO types.
/// </summary>
public class MapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MapperProfile"/> class
    /// and configures mappings between entities and DTOs.
    /// </summary>
    public MapperProfile()
    {
        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<Customer, CustomerDetailDTO>().ReverseMap();

        CreateMap<Insurance, InsuranceDTO>().ReverseMap();
        CreateMap<Insurance, InsuranceDetailDTO>().ReverseMap();

        CreateMap<InsuranceClaim, InsuranceClaimDTO>().ReverseMap();
    }
}
