using System;
using System.ComponentModel.DataAnnotations;
using Models;

public class ComicDtoIn
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Author { get; set; }

    [Required]
    [MaxLength(255)]
    public string Publisher { get; set; }

    [Required]
    public int YearPublished { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public bool IsForAdults { get; set; } = true;

    [Required]
    public Comic.ComicType Type { get; set; }
}
