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
using WebAPI.Services;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorsController : ControllerBase
    {
        private readonly IContributorService _contributorService;

        public ContributorsController(IContributorService contributorService)
        {
            _contributorService = contributorService;
        }

        // GET: api/Contributors
        [HttpGet]
        public async Task<ActionResult<List<ContributorViewModel>>> GetContributor()
        {
            return await _contributorService.Get();
        }

        // GET: api/Contributors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContributorViewModel>> GetContributor(Guid id)
        {
            var res = await _contributorService.Get(id);

            if (res == null)
                return NotFound();

            return res;
        }

        // PUT: api/Contributors
        [HttpPut]
        public async Task<IActionResult> PutContributor(ContributorViewModel contributor)
        {
            if (contributor.ID == null)
                return BadRequest();

            var res = await _contributorService.Put(contributor);

            if (res == null)
                return NotFound();

            return NoContent();
        }

        // POST: api/Contributors
        [HttpPost]
        public async Task<ActionResult<ContributorViewModel>> PostContributor(ContributorViewModel contributor)
        {
            ContributorViewModel resp = await _contributorService.Post(contributor);

            return CreatedAtAction("GetContributor", new { id = resp.ID }, resp);
        }

        // DELETE: api/Contributors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContributor(Guid id)
        {
            var res = await _contributorService.Delete(id);

            if (res == null)
                return NotFound();

            return NoContent();
        }
    }
}
