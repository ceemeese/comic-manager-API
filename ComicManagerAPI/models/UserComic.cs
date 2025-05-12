using System.ComponentModel.DataAnnotations;

namespace Models;

public class UserComic
{
    [Key]
    public int UserId { get; private set;}
    public required User User { get; set; }

    [Key]
    public int ComicId { get; private set; }
    public required Comic Comic { get; set; }



    public UserComic() {}


    public UserComic( User user, Comic comic)
    {
        User = user;
        UserId = user.Id;
        Comic = comic;
        ComicId = comic.Id; 
    }

}


