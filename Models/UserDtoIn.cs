
using System.ComponentModel.DataAnnotations;

namespace Models;

public class UserDtoIn
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [EmailAddress]
    [Required]
    [MaxLength(255)]
    public string Mail { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "La contraseña debe tener entre 6 y 10 caracteres", MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe tener 9 dígitos")]
    public string Telephone { get; set; }
}