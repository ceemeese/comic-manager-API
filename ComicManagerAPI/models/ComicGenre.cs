using System.ComponentModel.DataAnnotations;

namespace Models;

public class ComicGenre
{
    [Key]
    public int ComicId { get; private set;}
    public required Comic Comic { get; set; }

    [Key]
    public int GenreId { get; private set; }
    public required Genre Genre { get; set; }



    public ComicGenre() {}


    public ComicGenre( Comic comic, Genre genre)
    {
        Comic = comic;
        ComicId = comic.Id;
        Genre = genre;
        GenreId = genre.Id; 
    }

}


