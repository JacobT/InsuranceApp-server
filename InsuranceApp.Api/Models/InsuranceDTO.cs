using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Api.Models;

/// <summary>
/// Represents a lightweight data transfer object (DTO) for insurance information.
/// </summary>
public class InsuranceDTO : IDto
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public required string Name { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required, StringLength(50)]
    public required string Subject { get; set; }

    [Required]
    public DateTime ValidFrom { get; set; }

    [Required]
    public DateTime ValidTo { get; set; }

    [Required]
    public int InsuredId { get; set; }
}
