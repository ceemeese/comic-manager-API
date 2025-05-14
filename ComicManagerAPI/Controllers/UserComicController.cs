using ComicManagerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ComicManagerAPI.Controllers
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
        public async Task<ActionResult<UserComic>> CreateUserComic(UserComic usercomic)
        {
            await _serviceUserComic.AddAsync(usercomic);
            return CreatedAtAction(nameof(GetUserComic), new {userId = usercomic.UserId, comicId = usercomic.ComicId}, usercomic);
        }



        [HttpDelete("{userId}/{comicId}")]
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



        [HttpGet("users/{comicId}")]
        public async Task<ActionResult<List<User>>> GetUsersByComicId(int comicId)
        {
            return await _serviceUserComic.GetUsersByComicIdAsync(comicId);
        }


        [HttpGet("comics/{userId}")]
        public async Task<ActionResult<List<Comic>>> GetComicsByUserId(int userId)
        {
            return await _serviceUserComic.GetComicsByUserIdAsync(userId);
        }



        [HttpGet("{userId}/{comicId}")]
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