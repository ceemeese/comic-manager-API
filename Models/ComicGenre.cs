using System.ComponentModel.DataAnnotations;

namespace Models;

public class ComicGenre
{
    public int ComicId { get; set;}
    public Comic Comic { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; }



    public ComicGenre() {}


    public ComicGenre(int comicId, int genreId)
    {
        ComicId = comicId;
        GenreId = genreId; 
    }
}


