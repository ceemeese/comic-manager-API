using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utils;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDtoIn loginDtoIn)
        {
            try
            {
                /*if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }*/

                var token = await _authService.Login(loginDtoIn);
                return Ok(token);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Error de inicio de sesión para el usuario {Mail}", loginDtoIn.Mail);
                return Unauthorized("Usuario o contraseña incorrectos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar el token de {Mail}", loginDtoIn.Mail);
                return BadRequest("Error al generar token");
            }
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDtoIn userDtoIn)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var token = await _authService.Register(userDtoIn);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar el usuario {Mail}", userDtoIn.Mail);
                return BadRequest("Error al generar token: " + ex.Message);
            }
        }
    }
}