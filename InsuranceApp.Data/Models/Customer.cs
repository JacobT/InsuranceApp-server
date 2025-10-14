using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data.Models;

/// <summary>
/// Represents a customer who owns one or more <see cref="Insurance"/> policies.
/// </summary>
public class Customer : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public required string FirstName { get; set; }

    [Required, StringLength(50)]
    public required string LastName { get; set; }

    [Required, StringLength(50)]
    public required string Email { get; set; }

    [Required, StringLength(50)]
    public required string Phone { get; set; }

    [Required, StringLength(50)]
    public required string Street { get; set; }

    [Required, StringLength(50)]
    public required string City { get; set; }

    [Required, StringLength(6)]
    public required string PostalCode { get; set; }

    public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();
}
