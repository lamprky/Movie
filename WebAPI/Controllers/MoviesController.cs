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
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<List<MovieViewModel>>> GetMovie()
        {
            return await _movieService.Get();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieViewModel>> GetMovie(Guid id)
        {
            var res = await _movieService.Get(id); ;

            if (res == null)
                return NotFound();

            return res;
        }

        // PUT: api/Movies
        [HttpPut]
        public async Task<IActionResult> PutMovie(MovieViewModel movie)
        {
            if (movie.ID == null)
                return BadRequest();

            var res = await _movieService.Put(movie);

            if (res == null)
                return NotFound();

            return NoContent();
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<IActionResult> PostMovie(MovieViewModel movie)
        {
            var result = await _movieService.Post(movie);

            return CreatedAtAction("GetMovie", new { id = result.ID }, result);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(Guid id)
        {
            var res = await _movieService.Delete(id);

            if (res == null)
                return NotFound();

            return NoContent();
        }

    }
}
