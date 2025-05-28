using System.Security.Claims;
using Models;

namespace Business;

public interface IAuthService
{
    Task<string> Login(LoginDtoIn userDtoIn);
    Task<string> Register(UserDtoIn userDtoIn);
    public string GenerateToken(UserDtoOut userDTOOut);
    public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);
}