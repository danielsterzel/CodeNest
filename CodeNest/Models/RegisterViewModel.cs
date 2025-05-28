using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class RegisterViewModel
{
    [Required] [StringLength(50)]public required string UserName { get; set; }
    [Required] [EmailAddress] public required string Email { get; set; }
    [Required] [StringLength(50)] public required string FirstName { get; set; }
    [Required] [StringLength(50)] public required string LastName { get; set; }
    [Required] [StringLength(15)] public required string Phone { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(35,MinimumLength = 8)]
    public required string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Hasła nie zgadzają się.")]
    public required string ComparePassword { get; set; }

    [Required] public required string Role { get; set; }
}