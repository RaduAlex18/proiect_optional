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
    public class ModelsRelationTRCTController : ControllerBase
    {
        private readonly tableContext _dbContext;

        public ModelsRelationTRCTController(tableContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET endpoint
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var truckCargoTrailerRelations = await _dbContext.ModelsRelationsTRCT.ToListAsync();
            return Ok(truckCargoTrailerRelations);
        }

        // GET endpoint
        [HttpGet("byTruckId/{truckId}")]
        public async Task<IActionResult> GetByTruckId(Guid truckId)
        {
            var relation = await _dbContext.ModelsRelationsTRCT.FirstOrDefaultAsync(trct => trct.TruckId.Equals(truckId));

            if (relation == null)
            {
                return NotFound();
            }

            return Ok(relation);
        }

        // CREATE endpoint
        [HttpPost]
        public async Task<IActionResult> Add(ModelsRelationTRCT truckCargoTrailerRelation)
        {
            _dbContext.ModelsRelationsTRCT.Add(truckCargoTrailerRelation);
            await _dbContext.SaveChangesAsync();
            return Ok(truckCargoTrailerRelation);
        }

        // DELETE endpoint 
        [HttpDelete("byTruckId/{truckId}")]
        public async Task<IActionResult> DeleteByTruckId(Guid truckId)
        {
            var relationToRemove = await _dbContext.ModelsRelationsTRCT.FirstOrDefaultAsync(trct => trct.TruckId.Equals(truckId));

            if (relationToRemove != null)
            {
                _dbContext.ModelsRelationsTRCT.Remove(relationToRemove);
                await _dbContext.SaveChangesAsync();
            }

            return Ok(await _dbContext.ModelsRelationsTRCT.ToListAsync());
        }

        // UPDATE endpoint 
        [HttpPatch("byTruckId/{truckId}")]
        public async Task<IActionResult> PatchByTruckId(Guid truckId, [FromBody] JsonPatchDocument<ModelsRelationTRCT> truckCargoTrailerRelationPatch)
        {
            if (truckCargoTrailerRelationPatch != null)
            {
                var relationToUpdate = await _dbContext.ModelsRelationsTRCT.FirstOrDefaultAsync(trct => trct.TruckId.Equals(truckId));

                if (relationToUpdate != null)
                {
                    truckCargoTrailerRelationPatch.ApplyTo(relationToUpdate, ModelState);

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    await _dbContext.SaveChangesAsync();

                    return Ok(relationToUpdate);
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
