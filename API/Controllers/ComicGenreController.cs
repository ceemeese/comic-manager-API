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
        public async Task<ActionResult<ComicGenreDtoOut>> CreateComicGenre(ComicGenreDtoIn comicgenre)
        {
            try
            {
                var created = await _serviceComicGenre.AddAsync(comicgenre);
                return CreatedAtAction(nameof(GetComicGenre),
                    new { comicId = created.ComicId, genreId = created.GenreId }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        [HttpDelete("comics/{comicId}/genres/{genreId}")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult> DeleteComicGenre(int comicId, int genreId)
        {
            try
            {
                await _serviceComicGenre.DeleteAsync(comicId, genreId);
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



        [HttpGet("comics/{comicId}/genres", Name = "GetAllGenresByComicId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GenreDtoOut>>> GetGenresByComicId(int comicId)
        {
            try
            {
                var genres = await _serviceComicGenre.GetGenresByComicIdAsync(comicId);
                return Ok(genres);
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


        [HttpGet("genres/{genreId}/comics", Name = "GetAllComicsByGenreId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ComicDtoOut>>> GetComicsByGenreId(int genreId)
        {
            try
            {
                var comics = await _serviceComicGenre.GetComicsByGenreIdAsync(genreId);
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



        [HttpGet("{comicId}/{genreId}", Name = "GetComicGenreByIds")]
        [AllowAnonymous]
        public async Task<ActionResult<ComicGenre>> GetComicGenre(int comicId, int genreId)
        {
            try
            {
                var comicgenre = await _serviceComicGenre.GetByIdAsync(comicId, genreId);
                return Ok(comicgenre);
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
    }
}