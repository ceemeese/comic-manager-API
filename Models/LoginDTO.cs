using System.ComponentModel.DataAnnotations;

namespace Models;

public class LoginDtoIn
{
    [Required]
    public required string Mail { get; set; }
    [Required]
    public required string Password { get; set; }
}