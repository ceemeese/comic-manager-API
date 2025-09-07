using System.ComponentModel.DataAnnotations;

namespace Models;


public class GenreDtoIn
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; }

    [Required]
    public int Priority { get; set; }

    [Required]
    public string Icon { get; set; }
}