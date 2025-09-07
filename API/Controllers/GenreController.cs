using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        private readonly IGenreService _serviceGenre;
        private readonly ILogger<GenresController> _logger;


        public GenresController(IGenreService service, ILogger<GenresController> logger)
        {
            _serviceGenre = service;
            _logger = logger;
        }


        [HttpGet (Name = "GetAllGenres")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Genre>>> GetAllGenres()
        {
            try
            {
                var genres = await _serviceGenre.GetAllAsync();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los géneros");
                return StatusCode(500, "Error interno del servidor al obtener todos los géneros");
            }
        }

        [HttpGet("{id}", Name = "GetGenre")]
        [AllowAnonymous]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            try
            {
                var genre = await _serviceGenre.GetByIdAsync(id);
                return Ok(genre);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Género con ID {id} no encontrado");
                return NotFound($"Género con ID {id} no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error interno del servidor al obtener el género con ID {id}");
                return StatusCode(500, "Error interno del servidor al obtener el género");
            }
        }

        [HttpPost]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult<GenreDtoOut>> CreateGenre(GenreDtoIn genreDto)
        {
            try
            {
                var genre = await _serviceGenre.AddAsync(genreDto);
                return CreatedAtAction(nameof(GetGenre), new {id = genre.Id}, genre);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Datos inválidos al crear el género con {genreName}", genreDto.Name);
                return BadRequest($"Datos inválidos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al crear el género {genreName}", genreDto.Name);
                return StatusCode(500, "Error interno del servidor al crear el género");
            }
        }   



        [HttpPut("{id}")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult> UpdateGenre(int id, GenreDtoIn genreDto)
        {
            try
            {
                await _serviceGenre.UpdateAsync(id, genreDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Género con ID {id} no encontrado para actualización", id);
                return NotFound($"Género con ID {id} no encontrado para la actualización");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al actualizar el género con ID {id}", id);
                return StatusCode(500, "Error interno del servidor al actualizar el género");
            }
        }


        [HttpDelete("{id}")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            try
            {
                await _serviceGenre.GetByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Género con ID {id} no encontrado para eliminación", id);
                return NotFound($"Género con ID {id} no encontrado para la eliminación");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al eliminar el género con ID {id}", id);
                return StatusCode(500, "Error interno del servidor al eliminar el género");
            }
        }

    }
}