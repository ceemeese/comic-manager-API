using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {

        private readonly IComicService _serviceComic;


        public ComicsController(IComicService service)
        {
            _serviceComic = service;
        }


        [HttpGet (Name = "GetAllComics")]
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
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                return BadRequest($"Datos inválidos: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
            
        }


        [HttpPut("{id}")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult<Comic>> UpdateComic(int id, ComicDtoIn comicDto)
        {
            try
            {
                await _serviceComic.UpdateAsync(id, comicDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}