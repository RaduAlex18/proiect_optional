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
    public class RoutesController : ControllerBase
    {
        public static List<Routes> routesList = new List<Routes>
        {
            new Routes { Id = Guid.NewGuid(), km = 200, city_start = "City A", city_end = "City B" },
            new Routes { Id = Guid.NewGuid(), km = 150, city_start = "City C", city_end = "City D" },
            new Routes { Id = Guid.NewGuid(), km = 300, city_start = "City E", city_end = "City F" },
            new Routes { Id = Guid.NewGuid(), km = 120, city_start = "City G", city_end = "City H" },
            new Routes { Id = Guid.NewGuid(), km = 250, city_start = "City I", city_end = "City J" },
        };

        // GET endpoint
        [HttpGet]
        public List<Routes> Get()
        {
            return routesList;
        }

        [HttpGet("byId")]
        public Routes Get(int id)
        {
            return routesList.FirstOrDefault(r => r.Id.Equals(id));
        }

        [HttpGet("byId/{id}")]
        public Routes GetByIdInEndpoint(int id)
        {
            return routesList.FirstOrDefault(r => r.Id.Equals(id));
        }

        [HttpGet("filter/{start}/{end}")]
        public Routes GetWithFilters(string start, string end)
        {
            return routesList.FirstOrDefault(r => r.city_start.Equals(start) && r.city_end.Equals(end));
        }

        [HttpGet("fromRouteWithId/{id}")]
        public Routes GetByIdWithFromRoute([FromRoute] int id)
        {
            return routesList.FirstOrDefault(r => r.Id.Equals(id));
        }

        [HttpGet("fromHeader")]
        public Routes GetByIdWithFromHeader([FromHeader] int id)
        {
            return routesList.FirstOrDefault(r => r.Id.Equals(id));
        }

        [HttpGet("fromQuery")]
        public Routes GetByIdWithFromQuery([FromQuery] int id)
        {
            return routesList.FirstOrDefault(r => r.Id.Equals(id));
        }

        [HttpGet("fromQueryAsync")]
        public IActionResult GetByIdWithFromQueryAsync([FromQuery] int id)
        {
            return Ok(routesList.FirstOrDefault(r => r.Id.Equals(id)));
        }

        // CREATE endpoint
        [HttpPost]
        public List<Routes> Add(Routes route)
        {
            routesList.Add(route);
            return routesList;
        }

        [HttpPost("fromBody")]
        public IActionResult AddWithFromBody([FromBody] Routes route)
        {
            routesList.Add(route);
            return Ok(routesList);
        }

        [HttpPost("fromForm")]
        public IActionResult AddWithFromForm([FromForm] Routes route)
        {
            routesList.Add(route);
            return Ok(routesList);
        }

        // DELETE endpoint
        [HttpDelete]
        public List<Routes> Delete(Routes route)
        {
            var routeIndex = routesList.FindIndex(r => r.Id == route.Id);
            routesList.RemoveAt(routeIndex);
            return routesList;
        }

        // UPDATE endpoint (Partial Update)
        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] JsonPatchDocument<Routes> route)
        {
            if (route != null)
            {
                var routeToUpdate = routesList.FirstOrDefault(r => r.Id.Equals(id));
                route.ApplyTo(routeToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                return Ok(routesList);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
