using System.Security.Claims;
using Models;

namespace Business;

public interface IAuthService
{
    public string Login(LoginDtoIn userDtoIn);
    public string Register(UserDtoIn userDtoIn);
    public string GenerateToken(UserDtoOut userDTOOut);
    public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);
}