using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Api.Models;

/// <summary>
/// Represents a detailed data transfer object (DTO) for customer information, 
/// including associated insurances.
/// </summary>
public class CustomerDetailDTO : CustomerDTO
{
    [Required]
    public IList<InsuranceDTO> Insurances { get; set; } = new List<InsuranceDTO>();
}
