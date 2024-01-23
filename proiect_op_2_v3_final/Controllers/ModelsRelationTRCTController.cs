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
    public class TruckCargoTrailerController : ControllerBase
    {
        public static List<Truck> trucks = new List<Truck>
        {
            new Truck { Id = Guid.NewGuid(), Brand_T = "Volvo", Year = 2020, Color = "Blue" },
            new Truck { Id = Guid.NewGuid(), Brand_T = "Scania", Year = 2019, Color = "Red" },
            new Truck { Id = Guid.NewGuid(), Brand_T = "Mercedes", Year = 2021, Color = "White" },
        };

        public static List<CargoTrailer> cargoTrailers = new List<CargoTrailer>
        {
            new CargoTrailer { Id = Guid.NewGuid(), Brand = "Schmitz", Type = "Box Trailer", Year = 2020, Color = "Blue" },
            new CargoTrailer { Id = Guid.NewGuid(), Brand = "Krone", Type = "Flatbed Trailer", Year = 2019, Color = "Red" },
            new CargoTrailer { Id = Guid.NewGuid(), Brand = "Thermo King", Type = "Refrigerated Trailer", Year = 2021, Color = "White" },
        };

        public static List<ModelsRelationTRCT> truckCargoTrailerRelations = new List<ModelsRelationTRCT>
        {
            new ModelsRelationTRCT { TruckId = trucks[0].Id, CargoTrailerId = cargoTrailers[0].Id },
            new ModelsRelationTRCT { TruckId = trucks[1].Id, CargoTrailerId = cargoTrailers[1].Id },
            new ModelsRelationTRCT { TruckId = trucks[2].Id, CargoTrailerId = cargoTrailers[2].Id },
        };

        // GET endpoint
        [HttpGet]
        public List<ModelsRelationTRCT> Get()
        {
            return truckCargoTrailerRelations;
        }

        // GET endpoint
        [HttpGet("byTruckId/{truckId}")]
        public ModelsRelationTRCT GetByTruckId(Guid truckId)
        {
            return truckCargoTrailerRelations.FirstOrDefault(trct => trct.TruckId.Equals(truckId));
        }

        // CREATE endpoint
        [HttpPost]
        public List<ModelsRelationTRCT> Add(ModelsRelationTRCT truckCargoTrailerRelation)
        {
            truckCargoTrailerRelations.Add(truckCargoTrailerRelation);
            return truckCargoTrailerRelations;
        }

        // DELETE endpoint
        [HttpDelete("byTruckId/{truckId}")]
        public List<ModelsRelationTRCT> DeleteByTruckId(Guid truckId)
        {
            var relationToRemove = truckCargoTrailerRelations.FirstOrDefault(trct => trct.TruckId.Equals(truckId));

            if (relationToRemove != null)
            {
                truckCargoTrailerRelations.Remove(relationToRemove);
            }

            return truckCargoTrailerRelations;
        }

        // UPDATE endpoint
        [HttpPatch("byTruckId/{truckId}")]
        public IActionResult PatchByTruckId(Guid truckId, [FromBody] JsonPatchDocument<ModelsRelationTRCT> truckCargoTrailerRelation)
        {
            if (truckCargoTrailerRelation != null)
            {
                var relationToUpdate = truckCargoTrailerRelations.FirstOrDefault(trct => trct.TruckId.Equals(truckId));

                if (relationToUpdate != null)
                {
                    truckCargoTrailerRelation.ApplyTo(relationToUpdate, ModelState);

                    if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }

                    return Ok(truckCargoTrailerRelations);
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
