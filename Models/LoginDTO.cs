using System.ComponentModel.DataAnnotations;

namespace Models;

public class LoginDtoIn
{
    [Required]
    public required string Mail { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "La contraseña debe ser de 10 caracteres")]
    public required string Password { get; set; }
}