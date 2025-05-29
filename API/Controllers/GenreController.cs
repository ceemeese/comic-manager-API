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
            var genres = await _serviceGenre.GetAllAsync();
            return Ok(genres);
        }

        [HttpGet("{id}", Name = "GetGenre")]
        [AllowAnonymous]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _serviceGenre.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
        {
            await _serviceGenre.AddAsync(genre);
            return CreatedAtAction(nameof(GetGenre), new {id = genre.Id}, genre);
        }


        [HttpPut("{id}")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult<Genre>> UpdateGenre(int id, Genre genre)
        {
            var isExistingGenre = await _serviceGenre.GetByIdAsync(id);
            if (isExistingGenre == null)
            {
                return NotFound();
            }
            
            isExistingGenre.Name = genre.Name;
            isExistingGenre.Description = genre.Description;
            isExistingGenre.Priority = genre.Priority;
            isExistingGenre.Icon = genre.Icon;  


            await _serviceGenre.UpdateAsync(isExistingGenre);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize (Roles = Rols.Admin)]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var genre = await _serviceGenre.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            await _serviceGenre.DeleteAsync(id);
            return NoContent();
        }

    }
}