using InsuranceApp.Api.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Api.Models;

/// <summary>
/// Represents a lightweight data transfer object (DTO) for insurance claim information.
/// </summary>
public class InsuranceClaimDTO : IDto
{
    public int Id { get; set; }

    [Required]
    public required string Description { get; set; }

    public int? Amount { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    [Required]
    public int InsuranceId { get; set; }
}
