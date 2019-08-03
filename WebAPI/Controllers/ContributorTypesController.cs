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
    public class ContributorTypesController : ControllerBase
    {
        private readonly IContributorTypeService _contributorTypeService;


        public ContributorTypesController(IContributorTypeService contributorTypeService)
        {
            _contributorTypeService = contributorTypeService;
        }

        // GET: api/ContributorTypes
        [HttpGet]
        public async Task<ActionResult<List<ContributorTypeViewModel>>> GetContributorsType()
        {
            return await _contributorTypeService.Get();
        }

        // GET: api/ContributorTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContributorTypeViewModel>> GetContributorType(Guid id)
        {
            var res = await _contributorTypeService.Get(id);

            if (res == null)
                return NotFound();

            return res;
        }

        // PUT: api/ContributorTypes
        [HttpPut]
        public async Task<IActionResult> PutContributorType(ContributorTypeViewModel contributorType)
        {
            if (contributorType.ID == null)
                return BadRequest();

            var res = await _contributorTypeService.Put(contributorType);

            if (res == null)
                return NotFound();

            return NoContent();
        }

        // POST: api/ContributorTypes
        [HttpPost]
        public async Task<ActionResult<ContributorTypeViewModel>> PostContributorType(ContributorTypeViewModel contributorType)
        {
            var result = await _contributorTypeService.Post(contributorType);

            return CreatedAtAction("GetContributorType", new { id = result.ID }, result);
        }

        // DELETE: api/ContributorTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContributorType(Guid id)
        {
            var res = await _contributorTypeService.Delete(id);

            if (res == null)
                return NotFound();

            return NoContent();
        }

    }
}
