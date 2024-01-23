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
    public class CargoTrailersController : ControllerBase
    {
        public static List<CargoTrailer> cargoTrailersList = new List<CargoTrailer>
        {
            new CargoTrailer { Id = Guid.NewGuid(), Brand = "Schmitz", Type = "Box Trailer", Year = 2020, Color = "Blue" },
            new CargoTrailer { Id = Guid.NewGuid(), Brand = "Krone", Type = "Flatbed Trailer", Year = 2019, Color = "Red" },
            new CargoTrailer { Id = Guid.NewGuid(), Brand = "Thermo King", Type = "Refrigerated Trailer", Year = 2021, Color = "White" },
            new CargoTrailer { Id = Guid.NewGuid(), Brand = "Magyar", Type = "Tanker Trailer", Year = 2018, Color = "Green" },
            new CargoTrailer { Id = Guid.NewGuid(), Brand = "Kögel", Type = "Drop Deck Trailer", Year = 2022, Color = "Yellow" },
        };

        // GET endpoint
        [HttpGet]
        public List<CargoTrailer> Get()
        {
            return cargoTrailersList;
        }

        [HttpGet("byId")]
        public CargoTrailer Get(int id)
        {
            return cargoTrailersList.FirstOrDefault(ct => ct.Id.Equals(id));
        }

        [HttpGet("byId/{id}")]
        public CargoTrailer GetByIdInEndpoint(int id)
        {
            return cargoTrailersList.FirstOrDefault(ct => ct.Id.Equals(id));
        }

        [HttpGet("filter/{brand}/{year}")]
        public CargoTrailer GetWithFilters(string brand, int year)
        {
            return cargoTrailersList.FirstOrDefault(ct => ct.Brand.Equals(brand) && ct.Year.Equals(year));
        }

        [HttpGet("fromRouteWithId/{id}")]
        public CargoTrailer GetByIdWithFromRoute([FromRoute] int id)
        {
            return cargoTrailersList.FirstOrDefault(ct => ct.Id.Equals(id));
        }

        [HttpGet("fromHeader")]
        public CargoTrailer GetByIdWithFromHeader([FromHeader] int id)
        {
            return cargoTrailersList.FirstOrDefault(ct => ct.Id.Equals(id));
        }

        [HttpGet("fromQuery")]
        public CargoTrailer GetByIdWithFromQuery([FromQuery] int id)
        {
            return cargoTrailersList.FirstOrDefault(ct => ct.Id.Equals(id));
        }

        [HttpGet("fromQueryAsync")]
        public IActionResult GetByIdWithFromQueryAsync([FromQuery] int id)
        {
            return Ok(cargoTrailersList.FirstOrDefault(ct => ct.Id.Equals(id)));
        }

        // CREATE endpoint
        [HttpPost]
        public List<CargoTrailer> Add(CargoTrailer cargoTrailer)
        {
            cargoTrailersList.Add(cargoTrailer);
            return cargoTrailersList;
        }

        [HttpPost("fromBody")]
        public IActionResult AddWithFromBody([FromBody] CargoTrailer cargoTrailer)
        {
            cargoTrailersList.Add(cargoTrailer);
            return Ok(cargoTrailersList);
        }

        [HttpPost("fromForm")]
        public IActionResult AddWithFromForm([FromForm] CargoTrailer cargoTrailer)
        {
            cargoTrailersList.Add(cargoTrailer);
            return Ok(cargoTrailersList);
        }

        // DELETE endpoint
        [HttpDelete]
        public List<CargoTrailer> Delete(CargoTrailer cargoTrailer)
        {
            var cargoTrailerIndex = cargoTrailersList.FindIndex(ct => ct.Id == cargoTrailer.Id);
            cargoTrailersList.RemoveAt(cargoTrailerIndex);
            return cargoTrailersList;
        }

        // UPDATE endpoint (Partial Update)
        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] JsonPatchDocument<CargoTrailer> cargoTrailer)
        {
            if (cargoTrailer != null)
            {
                var cargoTrailerToUpdate = cargoTrailersList.FirstOrDefault(ct => ct.Id.Equals(id));
                cargoTrailer.ApplyTo(cargoTrailerToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                return Ok(cargoTrailersList);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
