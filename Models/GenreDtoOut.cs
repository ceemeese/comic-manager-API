

namespace Models;

public class GenreDtoOut
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PercentageOfComics { get; set; }
    public bool IsPopular { get; set; }
}