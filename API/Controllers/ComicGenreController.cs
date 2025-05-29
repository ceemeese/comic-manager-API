using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
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
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult<ComicGenre>> CreateComicGenre(ComicGenre comicgenre)
        {
            await _serviceComicGenre.AddAsync(comicgenre);
            return CreatedAtAction(nameof(GetComicGenre), new {comicId = comicgenre.ComicId, userId = comicgenre.GenreId}, comicgenre);
        }



        [HttpDelete("comics/{comicId}/genres/{genreId}")]
        [Authorize (Roles = Rols.Admin)]
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



        [HttpGet("comics/{comicId}/genres", Name = "GetAllGenresByComicId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenresByComicId(int comicId)
        {
            var genres = await _serviceComicGenre.GetGenresByComicIdAsync(comicId);
            return Ok(genres);
        }


        [HttpGet("genres/{genreId}/comics", Name = "GetAllComicsByGenreId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Comic>>> GetComicsByGenreId(int genreId)
        {
            var comics = await _serviceComicGenre.GetComicsByGenreIdAsync(genreId);
            return Ok(comics);
        }



        [HttpGet("{comicId}/{genreId}", Name = "GetComicGenreByIds")]
        [AllowAnonymous]
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