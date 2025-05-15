using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

class InvalidUserException: Exception 
{
    public InvalidUserException(string message = ""):base(message) 
    {

    }
}

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    [EmailAddress]
    [Required]
    [MaxLength(255)]
    public string Mail { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public DateTime? DateCreated { get; private set; }
    [Required]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe tener 9 dígitos")]
    public string Telephone { get; set; }
    public bool IsAdmin { get; set; } = false;
    public List<UserComic> UserComics { get; set; } 
    

    public User() 
    {
        DateCreated = DateTime.Now;
        UserComics = new List<UserComic>();
    }

    //Constructor
    public User(string name, string mail, string password, string telephone, bool isAdmin, DateTime? dateCreated = null ) 
    {
        Name = name;
        Mail = mail;
        Password = password;
        DateCreated = dateCreated ?? DateTime.Now;
        Telephone = telephone;
        IsAdmin = isAdmin;
        UserComics = new List<UserComic>();
    }

}