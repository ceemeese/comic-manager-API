using System.ComponentModel.DataAnnotations;

namespace Models;

public class ComicDtoOut
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int YearPublished { get; set; }
    public string Type { get; set; }
    public DateTime DateCreated { get; set; } 
    public bool? IsForAdults { get; set; }
    public decimal? Price { get; set; }
}   