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
    public class TruckController : ControllerBase
    {
        private readonly tableContext _dbContext;

        public TruckController(tableContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET endpoint
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trucks = await _dbContext.Trucks.ToListAsync();
            return Ok(trucks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var truck = await _dbContext.Trucks.FirstOrDefaultAsync(t => t.Id == id);

            if (truck == null)
            {
                return NotFound();
            }

            return Ok(truck);
        }

        [HttpGet("filter/{brand}/{year}")]
        public async Task<IActionResult> GetWithFilters(string brand, int year)
        {
            var truck = await _dbContext.Trucks
                .FirstOrDefaultAsync(t => t.Brand_T.Equals(brand) && t.Year == year);

            if (truck == null)
            {
                return NotFound();
            }

            return Ok(truck);
        }

        // CREATE endpoint
        [HttpPost]
        public async Task<IActionResult> Add(Truck truck)
        {
            _dbContext.Trucks.Add(truck);
            await _dbContext.SaveChangesAsync();
            return Ok(truck);
        }

        // DELETE endpoint
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var truck = await _dbContext.Trucks.FirstOrDefaultAsync(t => t.Id == id);

            if (truck == null)
            {
                return NotFound();
            }

            _dbContext.Trucks.Remove(truck);
            await _dbContext.SaveChangesAsync();

            return Ok(truck);
        }

        // PATCH endpoint
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Truck> truckPatch)
        {
            if (truckPatch != null)
            {
                var truckToUpdate = await _dbContext.Trucks.FirstOrDefaultAsync(t => t.Id == id);

                if (truckToUpdate == null)
                {
                    return NotFound();
                }

                truckPatch.ApplyTo(truckToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _dbContext.SaveChangesAsync();

                return Ok(truckToUpdate);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
