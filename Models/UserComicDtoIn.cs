

namespace Models;

public class UserComicDtoIn
{
    public int UserId { get; set; }
    public int ComicId { get; set; }
    public bool IsRead { get; set; } = false;
}