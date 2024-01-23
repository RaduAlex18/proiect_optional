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
    public class DriversController : ControllerBase
    {
        public static List<Driver> drivers = new List<Driver>
        {
            new Driver { Id = Guid.NewGuid(), First_Name = "John", Last_Name = "Doe", CNP = "123456789", Age = 30 },
            new Driver { Id = Guid.NewGuid(), First_Name = "Jane", Last_Name = "Smith", CNP = "987654321", Age = 25 },
            new Driver { Id = Guid.NewGuid(), First_Name = "Bob", Last_Name = "Johnson", CNP = "456789123", Age = 35 },
            new Driver { Id = Guid.NewGuid(), First_Name = "Alice", Last_Name = "Williams", CNP = "789123456", Age = 28 },
            new Driver { Id = Guid.NewGuid(), First_Name = "Charlie", Last_Name = "Brown", CNP = "321654987", Age = 32 },
        };

        // GET endpoint
        [HttpGet]
        public List<Driver> Get()
        {
            return drivers;
        }

        [HttpGet("byId")]
        public Driver Get(int id)
        {
            return drivers.FirstOrDefault(d => d.Id.Equals(id));
        }

        [HttpGet("byId/{id}")]
        public Driver GetByIdInEndpoint(int id)
        {
            return drivers.FirstOrDefault(d => d.Id.Equals(id));
        }

        [HttpGet("filter/{firstName}/{age}")]
        public Driver GetWithFilters(string firstName, int age)
        {
            return drivers.FirstOrDefault(d => d.First_Name.Equals(firstName) && d.Age.Equals(age));
        }

        [HttpGet("fromRouteWithId/{id}")]
        public Driver GetByIdWithFromRoute([FromRoute] int id)
        {
            return drivers.FirstOrDefault(d => d.Id.Equals(id));
        }

        [HttpGet("fromHeader")]
        public Driver GetByIdWithFromHeader([FromHeader] int id)
        {
            return drivers.FirstOrDefault(d => d.Id.Equals(id));
        }

        [HttpGet("fromQuery")]
        public Driver GetByIdWithFromQuery([FromQuery] int id)
        {
            return drivers.FirstOrDefault(d => d.Id.Equals(id));
        }

        [HttpGet("fromQueryAsync")]
        public IActionResult GetByIdWithFromQueryAsync([FromQuery] int id)
        {
            return Ok(drivers.FirstOrDefault(d => d.Id.Equals(id)));
        }

        // CREATE endpoint
        [HttpPost]
        public List<Driver> Add(Driver driver)
        {
            drivers.Add(driver);
            return drivers;
        }

        [HttpPost("fromBody")]
        public IActionResult AddWithFromBody([FromBody] Driver driver)
        {
            drivers.Add(driver);
            return Ok(drivers);
        }

        [HttpPost("fromForm")]
        public IActionResult AddWithFromForm([FromForm] Driver driver)
        {
            drivers.Add(driver);
            return Ok(drivers);
        }

        // DELETE endpoint
        [HttpDelete]
        public List<Driver> Delete(Driver driver)
        {
            var driverIndex = drivers.FindIndex(d => d.Id == driver.Id);
            drivers.RemoveAt(driverIndex);
            return drivers;
        }

        // UPDATE endpoint (Partial Update)
        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] JsonPatchDocument<Driver> driver)
        {
            if (driver != null)
            {
                var driverToUpdate = drivers.FirstOrDefault(d => d.Id.Equals(id));
                driver.ApplyTo(driverToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                return Ok(drivers);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
