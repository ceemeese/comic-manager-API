using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _serviceUser;
        private readonly ILogger<UsersController> _logger;


        public UsersController(IUserService service, ILogger<UsersController> logger)
        {
            _serviceUser = service;
            _logger = logger;
        }


        [HttpGet (Name = "GetAllUsers")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult<List<UserDtoOut>>> GetAllUsers()
        {
            try
            {
                var users = await _serviceUser.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {    
                _logger.LogError(ex, "Error al obtener todos los usuarios");
                return StatusCode(500, "Error interno del servidor");
            }  
        }

        [HttpGet("{id}", Name = "GetUser")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult<UserDtoOut>> GetUser(int id)
        {
            try
            {
                var user = await _serviceUser.GetByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Usuario con ID {id} no encontrado", id);
                return NotFound($"Usuario con ID {id} no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener el usuario con ID {id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }



        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(int id, UserDtoIn userDto)
        {
            try
            {
                await _serviceUser.UpdateAsync(id, userDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Usuario con ID {id} no encontrado", id);
                return NotFound($"Usuario con ID {id} no encontrado");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Datos inválidos proporcionados para el usuario con ID {id}", id);
                return BadRequest($"Datos inválidos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al actualizar el usuario con ID {id}", id);
                return StatusCode(500, $"Error interno del servidor al actualizar el usuario con ID {id}");
            }
        }
            


        [HttpDelete("{id}")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _serviceUser.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Usuario con ID {id} no encontrado", id);
                return NotFound($"Usuario con ID {id} no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al eliminar el usuario con ID {id}", id);
                return StatusCode(500, $"Error interno del servidor al eliminar el usuario con ID {id}");
            }

            
        }

        [HttpPost("init")]
        [AllowAnonymous]
         public async Task<IActionResult> InitData()
        {
            try
            {
                await _serviceUser.InitDataAsync();
                return Ok("Datos de usuario iniciados correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al inicializar los datos de usuario");
                return StatusCode(500, "Error interno del servidor al inicializar los datos de usuario");
            }
            
        }

    }
}