using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserComicController : ControllerBase
    {

        private readonly IUserComicService _serviceUserComic;
        private readonly ILogger<UserComicController> _logger;


        public UserComicController(IUserComicService service, ILogger<UserComicController> logger)
        {
            _serviceUserComic = service;
            _logger = logger;
        }



        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<UserComicDtoOut>> CreateUserComic(UserComicDtoIn usercomic)
        {
            try
            {
                var created = await _serviceUserComic.AddAsync(usercomic);
                return CreatedAtAction(nameof(GetUserComic),
                new { userId = created.UserId, comicId = created.ComicId }, created);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Error al crear la relación usuario-cómic");
                return NotFound("Usuario o cómic no encontrado");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Relación cómic-género ya existe");
                return Conflict("Relación cómic-género ya existe");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Datos inválidos");
                return BadRequest("Datos inválidos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al crear relación usuario-cómic");
                return StatusCode(500, "Error interno del servidor al crear relación usuario-cómic");
            }
        }



        [HttpDelete("users/{userId}/comics/{comicId}")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult> DeleteUserComic(int userId, int comicId)
        {
            try
            {
                await _serviceUserComic.DeleteAsync(userId, comicId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Cómic o usuario no encontrado para eliminar la relación");
                return NotFound("Cómic o usuario no encontrado para eliminar la relación");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al eliminar la relación usuario-cómic");
                return StatusCode(500, "Error interno del servidor al eliminar la relación usuario-cómic");
            }
        }



        [HttpGet("comics/{comicId}/users", Name = "GetUsersByComicId")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<UserDtoOut>>> GetUsersByComicId(int comicId)
        {
            try
            {
                var users = await _serviceUserComic.GetUsersByComicIdAsync(comicId);
                return Ok(users);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Cómic no encontrado");
                return NotFound("Cómic no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener usuarios por ID de cómic");
                return StatusCode(500, "Error interno del servidor al obtener usuarios por ID de cómic");
            }
        }


        [HttpGet("users/{userId}/comics", Name = "GetComicsByUserId")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<Comic>>> GetComicsByUserId(int userId)
        {
            try
            {
                var comics = await _serviceUserComic.GetComicsByUserIdAsync(userId);
                return Ok(comics);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Usuario no encontrado");
                return NotFound("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener cómics por ID de usuario");
                return StatusCode(500, "Error interno del servidor al obtener cómics por ID de usuario");
            }
        }



        [HttpGet("users/{userId}/comics/{comicId}", Name = "GetUserComicByIds")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<UserComic>> GetUserComic(int userId, int comicId)
        {
            try
            {
                var usercomic = await _serviceUserComic.GetByIdAsync(userId, comicId);
                return Ok(usercomic);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Relación usuario-cómic no encontrada");
                return NotFound("Relación usuario-cómic no encontrada");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener la relación usuario-cómic");
                return StatusCode(500, "Error interno del servidor al obtener la relación usuario-cómic");
            }
        }


        [HttpPut("user/{userId}/comic/{comicId}")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult> UpdateUserComic(int userId, int comicId, UserComicDtoUpdate usercomicdto)
        {
            try
            {
                await _serviceUserComic.UpdateAsync(userId, comicId, usercomicdto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Relación usuario-cómic no encontrada para actualización");
                return NotFound("Relación usuario-cómic no encontrada para actualización");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Datos inválidos");
                return BadRequest("Datos inválidos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al actualizar la relación usuario-cómic");
                return StatusCode(500, "Error interno del servidor  al actualizar la relación usuario-cómic");
            }
        }
        
    }
}