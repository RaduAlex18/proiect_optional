using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_op_2_v3_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly tableContext _dbContext;

        public RoutesController(tableContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET endpoint
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var routesList = await _dbContext.Routess.ToListAsync();
            return Ok(routesList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var route = await _dbContext.Routess.FirstOrDefaultAsync(r => r.Id == id);

            if (route == null)
            {
                return NotFound();
            }

            return Ok(route);
        }

        [HttpGet("filter/{start}/{end}")]
        public async Task<IActionResult> GetWithFilters(string start, string end)
        {
            var route = await _dbContext.Routess
                .FirstOrDefaultAsync(r => r.city_start.Equals(start) && r.city_end.Equals(end));

            if (route == null)
            {
                return NotFound();
            }

            return Ok(route);
        }

        // CREATE endpoint
        [HttpPost]
        public async Task<IActionResult> Add(Routes route)
        {
            _dbContext.Routess.Add(route);
            await _dbContext.SaveChangesAsync();
            return Ok(route);
        }

        // DELETE endpoint
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var route = await _dbContext.Routess.FirstOrDefaultAsync(r => r.Id == id);

            if (route == null)
            {
                return NotFound();
            }

            _dbContext.Routess.Remove(route);
            await _dbContext.SaveChangesAsync();

            return Ok(route);
        }

        // UPDATE endpoint (Partial Update)
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Routes> routePatch)
        {
            if (routePatch != null)
            {
                var routeToUpdate = await _dbContext.Routess.FirstOrDefaultAsync(r => r.Id == id);

                if (routeToUpdate == null)
                {
                    return NotFound();
                }

                routePatch.ApplyTo(routeToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _dbContext.SaveChangesAsync();

                return Ok(routeToUpdate);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
