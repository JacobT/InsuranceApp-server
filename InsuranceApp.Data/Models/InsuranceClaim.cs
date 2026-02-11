using InsuranceApp.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceApp.Data.Models;

/// <summary>
/// Represents a insurance claim.
/// </summary>
public class InsuranceClaim : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public required string Description { get; set; }

    public int? Amount { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    [Required]
    [ForeignKey(nameof(Insurance))]
    public int InsuranceId { get; set; }
    public virtual Insurance Insurance { get; set; } = null!;
}
