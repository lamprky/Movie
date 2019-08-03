using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.Database;
using WebAPI.Models.Service;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<List<GenreViewModel>>> GetGenre()
        {
            return await _genreService.Get();
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreViewModel>> GetGenre(Guid id)
        {
            var res = await _genreService.Get(id); ;

            if (res == null)
                return NotFound();

            return res;
        }

        // PUT: api/Genres
        [HttpPut]
        public async Task<IActionResult> PutGenre(GenreViewModel genre)
        {
            if (genre.ID == null)
                return BadRequest();

            var res = await _genreService.Put(genre);

            if (res == null)
                return NotFound();

            return NoContent();
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<ActionResult<GenreViewModel>> PostGenre(GenreViewModel genre)
        {
            var result = await _genreService.Post(genre);

            return CreatedAtAction("GetGenre", new { id = result.ID }, result);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(Guid id)
        {
            var res = await _genreService.Delete(id);

            if (res == null)
                return NotFound();

            return NoContent();
        }
    }
}
