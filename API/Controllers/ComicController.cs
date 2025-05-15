using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ComicController : ControllerBase
    {

        private readonly IComicService _serviceComic;


        public ComicController(IComicService service)
        {
            _serviceComic = service;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var comics = await _serviceComic.GetAllAsync();
            return Ok(comics);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Comic>> GetComic(int id)
        {
            var comic = await _serviceComic.GetByIdAsync(id);
            if (comic == null)
            {
                return NotFound();
            }
            return Ok(comic);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Comic>> CreateComic(Comic comic)
        {
            await _serviceComic.AddAsync(comic);
            return CreatedAtAction(nameof(GetComic), new {id = comic.Id}, comic);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Comic>> UpdateComic(int id, Comic comic)
        {
            var isExistingComic = await _serviceComic.GetByIdAsync(id);
            if (isExistingComic == null)
            {
                return NotFound();
            }
            
            isExistingComic.Name = comic.Name;
            isExistingComic.Author = comic.Author;
            isExistingComic.Publisher= comic.Publisher;
            isExistingComic.YearPublished = comic.YearPublished;  
            isExistingComic.Price = comic.Price;  
            isExistingComic.IsRead = comic.IsRead;  
            isExistingComic.IsForAdults = comic.IsForAdults; 
            isExistingComic.Type = comic.Type;   


            await _serviceComic.UpdateAsync(isExistingComic);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteComic(int id)
        {
            var comic = await _serviceComic.GetByIdAsync(id);
            if (comic == null)
            {
                return NotFound();
            }

            await _serviceComic.DeleteAsync(id);
            return NoContent();
        }

    }
}