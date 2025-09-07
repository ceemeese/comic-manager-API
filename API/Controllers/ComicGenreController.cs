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
        private readonly ILogger<ComicGenreController> _logger;


        public ComicGenreController(IComicGenreService service, ILogger<ComicGenreController> logger)
        {
            _serviceComicGenre = service;
            _logger = logger;
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
                _logger.LogError(ex, "Comic o género no encontrado");
                return NotFound("Cómic o género no encontrado");
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
                _logger.LogError(ex, "Error interno del servidor al crear la relación cómic-género");
                return StatusCode(500, "Error interno del servidor al crear la relación cómic-género");
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
                _logger.LogError(ex, "Relación cómic-género no encontrada para eliminación");
                return NotFound("Relación cómic-género no encontrada");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al eliminar la relación cómic-género");
                return StatusCode(500, "Error interno del servidor al eliminar la relación cómic-género");
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
                _logger.LogError(ex, $"Cómic con ID {comicId} no encontrado");
                return NotFound("Cómic no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener géneros por ID de cómic");
                return StatusCode(500, "Error interno del servidor al obtener géneros por ID de cómic");
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
                _logger.LogError(ex, $"Género con ID {genreId} no encontrado");
                return NotFound("Género no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error interno del servidor al obtener cómics por ID de género");
                return StatusCode(500, "Error interno del servidor al obtener cómics por ID de género");
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
                _logger.LogError(ex, "Relación cómic-género no encontrada");
                return NotFound("Relación cómic-género no encontrada");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al obtener la relación cómic-género");
                return StatusCode(500, "Error interno del servidor al obtener la relación cómic-género");
            }  
        }
    }
}