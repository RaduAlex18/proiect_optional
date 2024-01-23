using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Models.Base;
using System.Collections.Generic;
using System.Linq;

namespace proiect_op_2_v3_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        public static List<City> cities = new List<City>
        {
            new City { Id = Guid.NewGuid(), Country = "USA", Name = "New York", Zip_code = 10001 },
            new City { Id = Guid.NewGuid(), Country = "UK", Name = "London", Zip_code = 32918 },
            new City { Id = Guid.NewGuid(), Country = "France", Name = "Paris", Zip_code = 75000 },
            new City { Id = Guid.NewGuid(), Country = "Germany", Name = "Berlin", Zip_code = 10115 },
            new City { Id = Guid.NewGuid(), Country = "Japan", Name = "Tokyo", Zip_code = 100-0001 },
        };

        // GET endpoint
        [HttpGet]
        public List<City> Get()
        {
            return cities;
        }

        [HttpGet("byId")]
        public City Get(int id)
        {
            return cities.FirstOrDefault(c => c.Id.Equals(id));
        }

        [HttpGet("byId/{id}")]
        public City GetByIdInEndpoint(int id)
        {
            return cities.FirstOrDefault(c => c.Id.Equals(id));
        }

        [HttpGet("filter/{country}/{zipCode}")]
        public City GetWithFilters(string country, int zipCode)
        {
            return cities.FirstOrDefault(c => c.Country.Equals(country) && c.Zip_code.Equals(zipCode));
        }

        [HttpGet("fromRouteWithId/{id}")]
        public City GetByIdWithFromRoute([FromRoute] int id)
        {
            return cities.FirstOrDefault(c => c.Id.Equals(id));
        }

        [HttpGet("fromHeader")]
        public City GetByIdWithFromHeader([FromHeader] int id)
        {
            return cities.FirstOrDefault(c => c.Id.Equals(id));
        }

        [HttpGet("fromQuery")]
        public City GetByIdWithFromQuery([FromQuery] int id)
        {
            return cities.FirstOrDefault(c => c.Id.Equals(id));
        }

        [HttpGet("fromQueryAsync")]
        public IActionResult GetByIdWithFromQueryAsync([FromQuery] int id)
        {
            return Ok(cities.FirstOrDefault(c => c.Id.Equals(id)));
        }

        // CREATE endpoint
        [HttpPost]
        public List<City> Add(City city)
        {
            cities.Add(city);
            return cities;
        }

        [HttpPost("fromBody")]
        public IActionResult AddWithFromBody([FromBody] City city)
        {
            cities.Add(city);
            return Ok(cities);
        }

        [HttpPost("fromForm")]
        public IActionResult AddWithFromForm([FromForm] City city)
        {
            cities.Add(city);
            return Ok(cities);
        }

        // DELETE endpoint
        [HttpDelete]
        public List<City> Delete(City city)
        {
            var cityIndex = cities.FindIndex(c => c.Id == city.Id);
            cities.RemoveAt(cityIndex);
            return cities;
        }

        // UPDATE endpoint (Partial Update)
        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] JsonPatchDocument<City> city)
        {
            if (city != null)
            {
                var cityToUpdate = cities.FirstOrDefault(c => c.Id.Equals(id));
                city.ApplyTo(cityToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                return Ok(cities);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
