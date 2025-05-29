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
        public async Task<ActionResult<UserComic>> CreateUserComic(UserComic usercomic)
        {
            await _serviceUserComic.AddAsync(usercomic);
            return CreatedAtAction(nameof(GetUserComic), new {userId = usercomic.UserId, comicId = usercomic.ComicId}, usercomic);
        }



        [HttpDelete("users/{userId}/comics/{comicId}")]
        [Authorize (Roles = "admin,user")]
        public async Task<ActionResult> DeleteUserComic(int userId, int comicId)
        {
            var usercomic = await _serviceUserComic.GetByIdAsync(userId, comicId);
            if (usercomic == null)
            {
                return NotFound();
            }

            await _serviceUserComic.DeleteAsync(userId, comicId);
            return NoContent();
        }



        [HttpGet("comics/{comicId}/users", Name ="GetUsersByComicId")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByComicId(int comicId)
        {
            var users = await _serviceUserComic.GetUsersByComicIdAsync(comicId);
            return Ok(users);
        }


        [HttpGet("users/{userId}/comics", Name ="GetComicsByUserId")]
        [Authorize (Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<Comic>>> GetComicsByUserId(int userId)
        {
            return await _serviceUserComic.GetComicsByUserIdAsync(userId);
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