using System.ComponentModel.DataAnnotations;


namespace InsuranceApp.Api.Models;

/// <summary>
/// Represents a detailed data transfer object (DTO) for insurance information, 
/// including associated claims.
/// </summary>
public class InsuranceDetailDTO : InsuranceDTO
{
    [Required]
    public IList<InsuranceClaimDTO> Claims { get; set; } = new List<InsuranceClaimDTO>();
}
