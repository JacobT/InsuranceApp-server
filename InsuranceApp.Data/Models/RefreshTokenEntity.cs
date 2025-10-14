using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data.Models;

/// <summary>
/// Represents a refresh token associated with a user.
/// Used to obtain new access tokens without requiring the user to log in again.
/// </summary>
[Index(nameof(RefreshTokenHash), IsUnique = true)]
[Index(nameof(UserId))]
[Table("RefreshToken")]
public class RefreshTokenEntity : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public required string UserId { get; set; }

    [Required]
    public required string RefreshTokenHash { get; set; }

    [NotMapped]
    public string? RefreshToken { get; set; }

    [Required]
    public DateTime ExpirationDate { get; set; }

    public virtual IdentityUser User { get; set; } = null!;
}
