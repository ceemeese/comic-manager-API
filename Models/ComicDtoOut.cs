using System.ComponentModel.DataAnnotations;

namespace Models;

public class ComicDtoOut
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public int YearPublished { get; set; }
    public bool IsRead { get; set; }
    public string Type { get; set; }
}   