using ComicManagerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ComicManagerAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ComicGenreController : ControllerBase
    {

        private readonly IComicGenreService _serviceComicGenre;


        public ComicGenreController(IComicGenreService service)
        {
            _serviceComicGenre = service;
        }



        [HttpPost]
        public async Task<ActionResult<ComicGenre>> CreateComicGenre(ComicGenre comicgenre)
        {
            await _serviceComicGenre.AddAsync(comicgenre);
            return CreatedAtAction(nameof(GetComicGenre), new {comicId = comicgenre.ComicId, userId = comicgenre.GenreId}, comicgenre);
        }



        [HttpDelete("{comicId}/{genreId}")]
        public async Task<ActionResult> DeleteComicGenre(int comicId, int genreId)
        {
            var comicgenre = await _serviceComicGenre.GetByIdAsync(comicId, genreId);
            if (comicgenre == null)
            {
                return NotFound();
            }

            await _serviceComicGenre.DeleteAsync(comicId, genreId);
            return NoContent();
        }



        [HttpGet("genres/{comicId}")]
        public async Task<ActionResult<List<Genre>>> GetGenresByComicId(int comicId)
        {
            return await _serviceComicGenre.GetGenresByComicIdAsync(comicId);
        }


        [HttpGet("comics/{genreId}")]
        public async Task<ActionResult<List<Comic>>> GetComicsByGenreId(int genreId)
        {
            return await _serviceComicGenre.GetComicsByGenreIdAsync(genreId);
        }



        [HttpGet("{comicId}/{genreId}")]
        public async Task<ActionResult<ComicGenre>> GetComicGenre(int comicId, int genreId)
        {
            var comicgenre = await _serviceComicGenre.GetByIdAsync(comicId, genreId);
            if (comicgenre == null)
            {
                return NotFound();
            }
            return Ok(comicgenre);
        }
    }
}