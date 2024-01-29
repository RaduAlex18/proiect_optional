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
    public class GoodsController : ControllerBase
    {
        private readonly tableContext _dbContext;

        public GoodsController(tableContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET endpoint
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var goodsList = await _dbContext.Goodss.ToListAsync();
            return Ok(goodsList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var goods = await _dbContext.Goodss.FirstOrDefaultAsync(g => g.Id == id);

            if (goods == null)
            {
                return NotFound();
            }

            return Ok(goods);
        }

        [HttpGet("filter/{name}/{price}")]
        public async Task<IActionResult> GetWithFilters(string name, int price)
        {
            var goods = await _dbContext.Goodss
                .FirstOrDefaultAsync(g => g.Name.Equals(name) && g.Price == price);

            if (goods == null)
            {
                return NotFound();
            }

            return Ok(goods);
        }

        // CREATE endpoint
        [HttpPost]
        public async Task<IActionResult> Add(Goods goods)
        {
            _dbContext.Goodss.Add(goods);
            await _dbContext.SaveChangesAsync();
            return Ok(goods);
        }

        // DELETE endpoint
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var goods = await _dbContext.Goodss.FirstOrDefaultAsync(g => g.Id == id);

            if (goods == null)
            {
                return NotFound();
            }

            _dbContext.Goodss.Remove(goods);
            await _dbContext.SaveChangesAsync();

            return Ok(goods);
        }

        // UPDATE endpoint (Partial Update)
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Goods> goodsPatch)
        {
            if (goodsPatch != null)
            {
                var goodsToUpdate = await _dbContext.Goodss.FirstOrDefaultAsync(g => g.Id == id);

                if (goodsToUpdate == null)
                {
                    return NotFound();
                }

                goodsPatch.ApplyTo(goodsToUpdate, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _dbContext.SaveChangesAsync();

                return Ok(goodsToUpdate);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
