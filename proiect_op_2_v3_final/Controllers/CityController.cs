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
    public class CityController : ControllerBase
    {
        private readonly tableContext _dbContext;

        public CityController(tableContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET endpoint
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _dbContext.Cities.ToListAsync();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpGet("filter/{country}/{zipCode}")]
        public async Task<IActionResult> GetWithFilters(string country, int zipCode)
        {
            var city = await _dbContext.Cities
                .FirstOrDefaultAsync(c => c.Country.Equals(country) && c.Zip_code == zipCode);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // CREATE endpoint
        [HttpPost]
        public async Task<IActionResult> Add(City city)
        {
            _dbContext.Cities.Add(city);
            await _dbContext.SaveChangesAsync();
            return Ok(city);
        }

        // DELETE endpoint
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();

            return Ok(city);
        }

        // UPDATE endpoint
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<City> cityPatch)
        {
            if (cityPatch != null)
            {
                var cityToUpdate = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);

                if (cityToUpdate == null)
                {
                    return NotFound();
                }

                cityPatch.ApplyTo(cityToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _dbContext.SaveChangesAsync();

                return Ok(cityToUpdate);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
