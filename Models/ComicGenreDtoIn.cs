using System.ComponentModel.DataAnnotations;

namespace Models;

public class ComicGenreDtoIn
{
    public int ComicId { get; set; }
    public int GenreId { get; set; }
}