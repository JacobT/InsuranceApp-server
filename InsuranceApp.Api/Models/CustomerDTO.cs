using InsuranceApp.Api.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Api.Models;

/// <summary>
/// Represents a lightweight data transfer object (DTO) for customer information.
/// </summary>
public class CustomerDTO :IDto
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public required string FirstName { get; set; }

    [Required, StringLength(50)]
    public required string LastName { get; set; }

    [Required, StringLength(50)]
    [EmailAddress]
    public required string Email { get; set; }

    [Required, StringLength(50)]
    [Phone]
    public required string Phone { get; set; }

    [Required, StringLength(50)]
    public required string Street { get; set; }

    [Required, StringLength(50)]
    public required string City { get; set; }

    /// <remarks>
    /// Valid postal codes must match the regex <c>^[1-7]\d{2}\s?\d{2}$</c>, 
    /// which enforces a specific national format.
    /// </remarks>
    [Required, StringLength(6)]
    [RegularExpression(@"^[1-7]\d{2}\s?\d{2}$", ErrorMessage = "Invalid postal code format.")]
    public required string PostalCode { get; set; }
}
