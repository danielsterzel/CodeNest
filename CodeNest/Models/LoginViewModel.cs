using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class LoginViewModel
{
    [Required] public required string Identifier { get; set; }

    [Required] public required string Role { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

}