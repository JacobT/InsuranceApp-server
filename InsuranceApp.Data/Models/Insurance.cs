using InsuranceApp.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InsuranceApp.Data.Models;

/// <summary>
/// Represents a insurance who owns one or more <see cref="InsuranceClaim"/>.
/// </summary>
public class Insurance : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    [ForeignKey(nameof(Customer))]
    public int InsuredId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<InsuranceClaim> Claims { get; set; } = new List<InsuranceClaim>();
}
