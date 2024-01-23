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
    public class GoodsController : ControllerBase
    {
        public static List<Goods> goodsList = new List<Goods>
        {
            new Goods { Id = Guid.NewGuid(), Name = "Diesel", Quantity = 10, Price = 1200 },
            new Goods { Id = Guid.NewGuid(), Name = "Petrol", Quantity = 20, Price = 800 },
            new Goods { Id = Guid.NewGuid(), Name = "Tablet", Quantity = 15, Price = 500 },
            new Goods { Id = Guid.NewGuid(), Name = "Camera", Quantity = 5, Price = 1000 },
            new Goods { Id = Guid.NewGuid(), Name = "Printer", Quantity = 8, Price = 300 },
        };

        // GET endpoint
        [HttpGet]
        public List<Goods> Get()
        {
            return goodsList;
        }

        [HttpGet("byId")]
        public Goods Get(int id)
        {
            return goodsList.FirstOrDefault(g => g.Id.Equals(id));
        }

        [HttpGet("byId/{id}")]
        public Goods GetByIdInEndpoint(int id)
        {
            return goodsList.FirstOrDefault(g => g.Id.Equals(id));
        }

        [HttpGet("filter/{name}/{price}")]
        public Goods GetWithFilters(string name, int price)
        {
            return goodsList.FirstOrDefault(g => g.Name.Equals(name) && g.Price.Equals(price));
        }

        [HttpGet("fromRouteWithId/{id}")]
        public Goods GetByIdWithFromRoute([FromRoute] int id)
        {
            return goodsList.FirstOrDefault(g => g.Id.Equals(id));
        }

        [HttpGet("fromHeader")]
        public Goods GetByIdWithFromHeader([FromHeader] int id)
        {
            return goodsList.FirstOrDefault(g => g.Id.Equals(id));
        }

        [HttpGet("fromQuery")]
        public Goods GetByIdWithFromQuery([FromQuery] int id)
        {
            return goodsList.FirstOrDefault(g => g.Id.Equals(id));
        }

        [HttpGet("fromQueryAsync")]
        public IActionResult GetByIdWithFromQueryAsync([FromQuery] int id)
        {
            return Ok(goodsList.FirstOrDefault(g => g.Id.Equals(id)));
        }

        // CREATE endpoint
        [HttpPost]
        public List<Goods> Add(Goods goods)
        {
            goodsList.Add(goods);
            return goodsList;
        }

        [HttpPost("fromBody")]
        public IActionResult AddWithFromBody([FromBody] Goods goods)
        {
            goodsList.Add(goods);
            return Ok(goodsList);
        }

        [HttpPost("fromForm")]
        public IActionResult AddWithFromForm([FromForm] Goods goods)
        {
            goodsList.Add(goods);
            return Ok(goodsList);
        }

        // DELETE endpoint
        [HttpDelete]
        public List<Goods> Delete(Goods goods)
        {
            var goodsIndex = goodsList.FindIndex(g => g.Id == goods.Id);
            goodsList.RemoveAt(goodsIndex);
            return goodsList;
        }

        // UPDATE endpoint (Partial Update)
        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] JsonPatchDocument<Goods> goods)
        {
            if (goods != null)
            {
                var goodsToUpdate = goodsList.FirstOrDefault(g => g.Id.Equals(id));
                goods.ApplyTo(goodsToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                return Ok(goodsList);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
