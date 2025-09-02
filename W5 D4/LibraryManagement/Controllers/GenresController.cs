using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly LibraryContext _context;
        public GenresController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.Include(g => g.Books).ToListAsync();
        }

        // GET: api/genres/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.Include(g => g.Books).FirstOrDefaultAsync(g => g.GenreId == id);
            if (genre == null) return NotFound();
            return genre;
        }

        // POST: api/genres
        [HttpPost]
        public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGenre), new { id = genre.GenreId }, genre);
        }

        // PUT: api/genres/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, Genre genre)
        {
            if (id != genre.GenreId) return BadRequest();
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/genres/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
