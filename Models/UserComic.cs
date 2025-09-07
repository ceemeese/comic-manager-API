using System.ComponentModel.DataAnnotations;

namespace Models;

public class UserComic
{
    public int UserId { get; set;}
    public User User { get; set; }

    public int ComicId { get; set; }
    public Comic Comic { get; set; }
    
    public bool IsRead { get; set; } = false;



    public UserComic() { }


    public UserComic( int userId, int comicId)
    {
        UserId = userId;
        ComicId = comicId; 
    }

}


