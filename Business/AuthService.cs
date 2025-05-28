using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Data.Repositories;
using Microsoft.Extensions.Configuration;
using Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace Business
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repository;



        public AuthService(IConfiguration configuration, IUserRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<string> Login(LoginDtoIn loginDtoIn)
        {
            var user = await _repository.GetUserByMail(loginDtoIn.Mail);

            if (user == null || !PasswordHasher.VerifyPassword(loginDtoIn.Password, user.Password ))
            {
                throw new KeyNotFoundException("Usuario o contraseña incorrectos");
            }

            var userLogged = new UserDtoOut()
            {   
                Id = user.Id,
                Name = user.Name,
                Mail = user.Mail,
                Telephone = user.Telephone,
                Role = user.Role,
            };

            return GenerateToken(userLogged);
        }

        public async Task<string> Register(UserDtoIn userDtoIn)
        {
            var hashedPassword = PasswordHasher.HashPassword(userDtoIn.Password);

            var user = await _repository.AddUserFromCredentials(userDtoIn, hashedPassword);

            var userRegistered = new UserDtoOut()
            {   
                Id = user.Id,
                Name = user.Name,
                Mail = user.Mail,
                Telephone = user.Telephone,
                Role = user.Role,
            };

            
            return GenerateToken(userRegistered);
        }

        public string GenerateToken(UserDtoOut userDTOOut)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userDTOOut.Id)),
                        new Claim(ClaimTypes.Name, userDTOOut.Name),
                        new Claim(ClaimTypes.Email, userDTOOut.Mail),
                        new Claim(ClaimTypes.MobilePhone, userDTOOut.Telephone),
                        new Claim(ClaimTypes.Role, userDTOOut.Role),
                        new Claim("myCustomClaim", "myCustomClaimValue"),
                        // add other claims
                    }),
                Expires = DateTime.UtcNow.AddDays(7), // AddMinutes(60)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return false;
            }
            var isOwnResource = userId == requestedUserID;

            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim != null) return false;
            var isAdmin = roleClaim!.Value == Rols.Admin;

            var hasAccess = isOwnResource || isAdmin;
            return hasAccess;
        }
    }
}