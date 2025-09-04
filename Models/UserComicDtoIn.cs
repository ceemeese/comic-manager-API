using System.ComponentModel.DataAnnotations;

namespace Models;

public class UserComicDtoIn
{
    public int UserId { get; set; }
    public int ComicId { get; set; }
}