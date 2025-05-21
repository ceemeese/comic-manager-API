using System.ComponentModel.DataAnnotations;

namespace Models;

public class UserDtoOut
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Mail { get; set; }

    public DateTime? DateCreated { get; set; }

    public string Telephone { get; set; }

    public bool IsAdmin { get; set; }
}

