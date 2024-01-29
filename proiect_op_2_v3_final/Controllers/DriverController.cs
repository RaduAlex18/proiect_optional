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
    public class DriverController : ControllerBase
    {
        private readonly tableContext _dbContext;

        public DriverController(tableContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET endpoint
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drivers = await _dbContext.Drivers.ToListAsync();
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var driver = await _dbContext.Drivers.FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        [HttpGet("filter/{firstName}/{age}")]
        public async Task<IActionResult> GetWithFilters(string firstName, int age)
        {
            var driver = await _dbContext.Drivers
                .FirstOrDefaultAsync(d => d.First_Name.Equals(firstName) && d.Age == age);

            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        // CREATE endpoint
        [HttpPost]
        public async Task<IActionResult> Add(Driver driver)
        {
            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();
            return Ok(driver);
        }

        // DELETE endpoint
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var driver = await _dbContext.Drivers.FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null)
            {
                return NotFound();
            }

            _dbContext.Drivers.Remove(driver);
            await _dbContext.SaveChangesAsync();

            return Ok(driver);
        }

        // UPDATE endpoint (Partial Update)
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Driver> driverPatch)
        {
            if (driverPatch != null)
            {
                var driverToUpdate = await _dbContext.Drivers.FirstOrDefaultAsync(d => d.Id == id);

                if (driverToUpdate == null)
                {
                    return NotFound();
                }

                driverPatch.ApplyTo(driverToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _dbContext.SaveChangesAsync();

                return Ok(driverToUpdate);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
