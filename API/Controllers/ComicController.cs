using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Serilog;
using Utils;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {

        private readonly IComicService _serviceComic;
        private readonly ILogger<ComicsController> _logger;


        public ComicsController(IComicService service, ILogger<ComicsController> logger)
        {
            _serviceComic = service;
            _logger = logger;
        }


        [HttpGet(Name = "GetAllComics")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ComicDtoOut>>> GetAllComics()
        {
            try
            {
                var comics = await _serviceComic.GetAllAsync();
                return Ok(comics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los cómics");
                return StatusCode(500, $"Error interno del servidor al obtener todos los cómics");
            }
        }


        [HttpGet("{id}", Name = "GetComic")]
        [AllowAnonymous]
        public async Task<ActionResult<ComicDtoOut>> GetComic(int id)
        {
            try
            {
                var comic = await _serviceComic.GetByIdAsync(id);
                return Ok(comic);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Cómic con ID {id} no encontrado");
                return NotFound("Cómic no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el cómic con ID {id}");
                return StatusCode(500, $"Error interno del servidor al obtener cómic con ID {id}");
            }
        }



        [HttpPost]
        [Authorize(Roles = Rols.Admin)]
        public async Task<ActionResult<ComicDtoOut>> CreateComic(ComicDtoIn comicDto)
        {
            try
            {
                var createdComic = await _serviceComic.AddAsync(comicDto);
                return CreatedAtAction(nameof(GetComic), new { id = createdComic.Id }, createdComic);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Datos inválidos al crear un nuevo cómic");
                return BadRequest($"Datos inválidos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al crear un nuevo cómic");
                return StatusCode(500, $"Error interno del servidor al crear un nuevo cómic");
            }

        }


        [HttpPut("{id}")]
        [Authorize(Roles = Rols.Admin)]
        public async Task<ActionResult> UpdateComic(int id, ComicDtoIn comicDto)
        {
            try
            {
                await _serviceComic.UpdateAsync(id, comicDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Cómic con ID {id} no encontrado para actualización");
                return NotFound("Cómic no encontrado");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Datos inválidos al actualizar el cómic");
                return BadRequest($"Datos inválidos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error interno del servidor al actualizar el cómic con ID {id}");
                return StatusCode(500, $"Error interno del servidor al actualizar el cómic con ID {id}");
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = Rols.Admin)]
        public async Task<ActionResult> DeleteComic(int id)
        {
            try
            {
                await _serviceComic.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Cómic con ID {id} no encontrado para eliminación");
                return NotFound("Cómic no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error interno del servidor al eliminar el cómic con ID {id}");
                return StatusCode(500, $"Error interno del servidor al eliminar el cómic con ID {id}");
            }
        }


        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ComicDtoOut>>> SearchComics([FromQuery] ComicQueryParameters queryParameters)
        {
            try
            {
                var comics = await _serviceComic.SearchComics(queryParameters);

                if (comics.Count == 0)
                    return NotFound("No se encontraron cómics que coincidan con los filtros de búsqueda");

                return Ok(comics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor al buscar cómics");
                return StatusCode(500, $"Error interno del servidor al buscar cómics");
            }
        }
    }
}