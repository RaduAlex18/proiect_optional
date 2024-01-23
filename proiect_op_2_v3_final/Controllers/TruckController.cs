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
    public class TruckController : ControllerBase
    {
        public static List<Truck> trucks = new List<Truck>
        {
            new Truck { Id = Guid.NewGuid(), Brand_T = "Volvo", Year = 2020, Color = "Blue" },
            new Truck { Id = Guid.NewGuid(), Brand_T = "Scania", Year = 2019, Color = "Red" },
            new Truck { Id = Guid.NewGuid(), Brand_T = "Mercedes", Year = 2021, Color = "White" },
            new Truck { Id = Guid.NewGuid(), Brand_T = "MAN", Year = 2018, Color = "Green" },
            new Truck { Id = Guid.NewGuid(), Brand_T = "Iveco", Year = 2022, Color = "Yellow" },
        };

        // GET endpoint
        [HttpGet]
        public List<Truck> Get()
        {
            return trucks;
        }

        [HttpGet("byId")]
        public Truck Get(int id)
        {
            return trucks.FirstOrDefault(t => t.Id.Equals(id));
        }

        [HttpGet("byId/{id}")]
        public Truck GetByIdInEndpoint(int id)
        {
            return trucks.FirstOrDefault(t => t.Id.Equals(id));
        }

        [HttpGet("filter/{brand}/{year}")]
        public Truck GetWithFilters(string brand, int year)
        {
            return trucks.FirstOrDefault(t => t.Brand_T.Equals(brand) && t.Year.Equals(year));
        }

        [HttpGet("fromRouteWithId/{id}")]
        public Truck GetByIdWithFromRoute([FromRoute] int id)
        {
            return trucks.FirstOrDefault(t => t.Id.Equals(id));
        }

        [HttpGet("fromHeader")]
        public Truck GetByIdWithFromHeader([FromHeader] int id)
        {
            return trucks.FirstOrDefault(t => t.Id.Equals(id));
        }

        [HttpGet("fromQuery")]
        public Truck GetByIdWithFromQuery([FromQuery] int id)
        {
            return trucks.FirstOrDefault(t => t.Id.Equals(id));
        }

        [HttpGet("fromQueryAsync")]
        public IActionResult GetByIdWithFromQueryAsync([FromQuery] int id)
        {
            return Ok(trucks.FirstOrDefault(t => t.Id.Equals(id)));
        }

        // CREATE endpoint
        [HttpPost]
        public List<Truck> Add(Truck truck)
        {
            trucks.Add(truck);
            return trucks;
        }

        [HttpPost("fromBody")]
        public IActionResult AddWithFromBody([FromBody] Truck truck)
        {
            trucks.Add(truck);
            return Ok(trucks);
        }

        [HttpPost("fromForm")]
        public IActionResult AddWithFromForm([FromForm] Truck truck)
        {
            trucks.Add(truck);
            return Ok(trucks);
        }

        // DELETE endpoint
        [HttpDelete]
        public List<Truck> Delete(Truck truck)
        {
            var truckIndex = trucks.FindIndex(t => t.Id == truck.Id);
            trucks.RemoveAt(truckIndex);
            return trucks;
        }

        // PATCH endpoint (Partial Update)
        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] JsonPatchDocument<Truck> truck)
        {
            if (truck != null)
            {
                var truckToUpdate = trucks.FirstOrDefault(t => t.Id.Equals(id));
                truck.ApplyTo(truckToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                return Ok(trucks);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
