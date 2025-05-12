using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

class InvalidComicException: Exception 
{
    public InvalidComicException(string message = ""):base(message) 
    {

    }
}

public class Comic 
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
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
    public bool IsRead { get; set; } = false;
    [Required]
    public bool IsForAdults { get; set; } = true;
    [Required]
    public DateTime? DateCreated { get; private set; }
    public List<Genre> Genres { get; set; } = new List<Genre>();
    [Required]
    public ComicType Type { get; set; }
    


    public enum ComicType
    {
        Americano = 1,
        Europeo = 2,
        Manga = 3,
        Manhwa = 4,
        Manhua = 5,
        Latinoamericano = 6,
        Webcomic = 7
    }

    public override string ToString()
    {
        return Name;
    }

    public Comic() {}

    //Constructor
    public Comic (string name, string author, string publisher, int yearPublished, decimal price,  bool isForAdults,   List<Genre> genres, ComicType type, DateTime? dateCreated = null)
    {
        Name = name;
        Author = author;
        Publisher = publisher;
        YearPublished = yearPublished;
        Price = price;
        IsForAdults = isForAdults;
        Genres = genres ?? new List<Genre>();
        Type = type;
        DateCreated = dateCreated ?? DateTime.Now;
    }

}