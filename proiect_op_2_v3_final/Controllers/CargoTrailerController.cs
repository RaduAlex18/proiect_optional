using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_op_2_v3_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoTrailerController : ControllerBase
    {
        private readonly tableContext _dbContext;

        public CargoTrailerController(tableContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET endpoint
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cargoTrailers = await _dbContext.CargoTrailers.ToListAsync();
            return Ok(cargoTrailers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cargoTrailer = await _dbContext.CargoTrailers.FirstOrDefaultAsync(ct => ct.Id == id);

            if (cargoTrailer == null)
            {
                return NotFound();
            }

            return Ok(cargoTrailer);
        }

        [HttpGet("filter/{brand}/{year}")]
        public async Task<IActionResult> GetWithFilters(string brand, int year)
        {
            var cargoTrailer = await _dbContext.CargoTrailers
                .FirstOrDefaultAsync(ct => ct.Brand.Equals(brand) && ct.Year == year);

            if (cargoTrailer == null)
            {
                return NotFound();
            }

            return Ok(cargoTrailer);
        }

        // CREATE endpoint
        [HttpPost]
        public async Task<IActionResult> Add(CargoTrailer cargoTrailer)
        {
            _dbContext.CargoTrailers.Add(cargoTrailer);
            await _dbContext.SaveChangesAsync();
            return Ok(cargoTrailer);
        }

        // DELETE endpoint
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cargoTrailer = await _dbContext.CargoTrailers.FirstOrDefaultAsync(ct => ct.Id == id);

            if (cargoTrailer == null)
            {
                return NotFound();
            }

            _dbContext.CargoTrailers.Remove(cargoTrailer);
            await _dbContext.SaveChangesAsync();

            return Ok(cargoTrailer);
        }

        // UPDATE endpoint
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<CargoTrailer> cargoTrailerPatch)
        {
            if (cargoTrailerPatch != null)
            {
                var cargoTrailerToUpdate = await _dbContext.CargoTrailers.FirstOrDefaultAsync(ct => ct.Id == id);

                if (cargoTrailerToUpdate == null)
                {
                    return NotFound();
                }

                cargoTrailerPatch.ApplyTo(cargoTrailerToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _dbContext.SaveChangesAsync();

                return Ok(cargoTrailerToUpdate);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
