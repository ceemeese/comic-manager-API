using ComicManagerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ComicManagerAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        private readonly IGenreService _serviceGenre;


        public GenreController(IGenreService service)
        {
            _serviceGenre = service;
        }


        [HttpGet]
        public async Task<ActionResult<List<Genre>>> GetGenres()
        {
            var genres = await _serviceGenre.GetAllAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
        {
            await _serviceGenre.AddAsync(genre);
            return CreatedAtAction(nameof(GetGenre), new {id = genre.Id}, genre);
        }


        [HttpPut("{id}")]
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