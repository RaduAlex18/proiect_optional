using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proiect_op_2_v3_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckRoutesController : ControllerBase
    {
        public static List<Truck> trucks = new List<Truck>
        {
            new Truck { Id = Guid.NewGuid(), Brand_T = "Volvo", Year = 2020, Color = "Blue" },
            new Truck { Id = Guid.NewGuid(), Brand_T = "Scania", Year = 2019, Color = "Red" },
            new Truck { Id = Guid.NewGuid(), Brand_T = "Mercedes", Year = 2021, Color = "White" },
        };

        public static List<Routes> routesList = new List<Routes>
        {
            new Routes { Id = Guid.NewGuid(), km = 200, city_start = "City A", city_end = "City B" },
            new Routes { Id = Guid.NewGuid(), km = 150, city_start = "City C", city_end = "City D" },
            new Routes { Id = Guid.NewGuid(), km = 300, city_start = "City E", city_end = "City F" },
        };

        public static List<ModelsRelationTRRT> truckRoutesRelations = new List<ModelsRelationTRRT>
        {
            new ModelsRelationTRRT { TruckId = trucks[0].Id, RoutesId = routesList[0].Id },
            new ModelsRelationTRRT { TruckId = trucks[1].Id, RoutesId = routesList[1].Id },
            new ModelsRelationTRRT { TruckId = trucks[2].Id, RoutesId = routesList[2].Id },
        };

        // GET endpoint
        [HttpGet]
        public List<ModelsRelationTRRT> Get()
        {
            return truckRoutesRelations.ToList();
        }

        // GET endpoint
        [HttpGet("byTruckId/{truckId}")]
        public ModelsRelationTRRT GetByTruckId(Guid truckId)
        {
            return truckRoutesRelations.FirstOrDefault(trrt => trrt.TruckId.Equals(truckId));
        }

        // Create endpoint
        [HttpPost]
        public List<ModelsRelationTRRT> Add(ModelsRelationTRRT truckRoutesRelation)
        {
            truckRoutesRelations.Add(truckRoutesRelation);
            return truckRoutesRelations;
        }

        // DELETE endpoint 
        [HttpDelete("byTruckId/{truckId}")]
        public List<ModelsRelationTRRT> DeleteByTruckId(Guid truckId)
        {
            var relationToRemove = truckRoutesRelations.FirstOrDefault(trrt => trrt.TruckId.Equals(truckId));

            if (relationToRemove != null)
            {
                truckRoutesRelations.Remove(relationToRemove);
            }

            return truckRoutesRelations;
        }

        // UPDATE endpoint 
        [HttpPatch("byTruckId/{truckId}")]
        public IActionResult PatchByTruckId(Guid truckId, [FromBody] JsonPatchDocument<ModelsRelationTRRT> truckRoutesRelation)
        {
            if (truckRoutesRelation != null)
            {
                var relationToUpdate = truckRoutesRelations.FirstOrDefault(trrt => trrt.TruckId.Equals(truckId));

                if (relationToUpdate != null)
                {
                    truckRoutesRelation.ApplyTo(relationToUpdate, ModelState);

                    if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }

                    return Ok(truckRoutesRelations);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
