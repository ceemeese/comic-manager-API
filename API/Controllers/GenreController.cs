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


        public GenresController(IGenreService service)
        {
            _serviceGenre = service;
        }


        [HttpGet (Name = "GetAllGenres")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            try
            {
                var genres = await _serviceGenre.GetAllAsync();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                return BadRequest($"Datos inválidos: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }   



        [HttpPut("{id}")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult<GenreDtoOut>> UpdateGenre(int id, GenreDtoIn genreDto)
        {
            try
            {
                await _serviceGenre.UpdateAsync(id, genreDto);
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}