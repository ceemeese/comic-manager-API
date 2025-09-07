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


        public UsersController(IUserService service)
        {
            _serviceUser = service;
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
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Datos inválidos: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
            
        }

    }
}