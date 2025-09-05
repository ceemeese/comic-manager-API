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


        public UserComicController(IUserComicService service)
        {
            _serviceUserComic = service;
        }



        [HttpPost]
        [Authorize (Roles = "admin,user")]
        public async Task<ActionResult<UserComicDtoOut>> CreateUserComic(UserComicDtoIn usercomic)
        {
            var created = await _serviceUserComic.AddAsync(usercomic);
            return CreatedAtAction(nameof(GetUserComic),
            new { userId = created.UserId, comicId = created.ComicId }, created);
        }



        [HttpDelete("users/{userId}/comics/{comicId}")]
        [Authorize (Roles = "admin,user")]
        public async Task<ActionResult> DeleteUserComic(int userId, int comicId)
        {
            try
            {
                await _serviceUserComic.DeleteAsync(userId, comicId);
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpGet("users/{userId}/comics", Name ="GetComicsByUserId")]
        [Authorize (Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<Comic>>> GetComicsByUserId(int userId)
        {
            try
            {
                var comics = await _serviceUserComic.GetComicsByUserIdAsync(userId);
                return Ok(comics);
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



        [HttpGet("users/{userId}/comics/{comicId}", Name ="GetUserComicByIds")]
        [Authorize (Roles = "admin,user")]
        public async Task<ActionResult<UserComic>> GetUserComic(int userId, int comicId)
        {
            var usercomic = await _serviceUserComic.GetByIdAsync(userId, comicId);
            if (usercomic == null)
            {
                return NotFound();
            }
            return Ok(usercomic);
        }
    }
}