using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

class InvalidGenreException: Exception 
{
    public InvalidGenreException(string message = ""):base(message) 
    {

    }
}

public class Genre
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    [Required]
    [MaxLength(255)]
    public string Description { get; set; }  
    [Required]
    public int Priority{ get; set; }
    [Required]
    public string Icon { get; set; }
    public List<ComicGenre> ComicGenres { get; set; } = new ();
    public DateTime? DateCreated { get; private set; }
    public decimal PercentageOfComics { get; private set; }
    public bool IsPopular { get; private set; } = false;

    public override string ToString()
    {
        return Name;
    }


    public Genre() {}


    public Genre (string name, string description, int priority, string icon, List<Comic>? comics = null, DateTime? dateCreated = null, decimal percentageOfComics = 0, bool isPopular = false) {
        Name = name;
        Description = description;
        Priority = priority;
        Icon = icon;
        DateCreated = dateCreated ?? DateTime.Now;
        PercentageOfComics = percentageOfComics;
        IsPopular = isPopular;
        ComicGenres = comics?.Select(c => new ComicGenre(c, this)).ToList() ?? new List<ComicGenre>();
    }

}