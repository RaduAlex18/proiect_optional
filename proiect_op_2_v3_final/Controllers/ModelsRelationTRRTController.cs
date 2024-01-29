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
    public class ModelsRelationTRRTController : ControllerBase
    {
        private readonly tableContext _dbContext;

        public ModelsRelationTRRTController(tableContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET endpoint
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var truckRoutesRelations = await _dbContext.ModelsRelationsTRRT.ToListAsync();
            return Ok(truckRoutesRelations);
        }

        // GET endpoint
        [HttpGet("byTruckId/{truckId}")]
        public async Task<IActionResult> GetByTruckId(Guid truckId)
        {
            var relation = await _dbContext.ModelsRelationsTRRT.FirstOrDefaultAsync(trrt => trrt.TruckId.Equals(truckId));

            if (relation == null)
            {
                return NotFound();
            }

            return Ok(relation);
        }

        // Create endpoint
        [HttpPost]
        public async Task<IActionResult> Add(ModelsRelationTRRT truckRoutesRelation)
        {
            _dbContext.ModelsRelationsTRRT.Add(truckRoutesRelation);
            await _dbContext.SaveChangesAsync();
            return Ok(truckRoutesRelation);
        }

        // DELETE endpoint 
        [HttpDelete("byTruckId/{truckId}")]
        public async Task<IActionResult> DeleteByTruckId(Guid truckId)
        {
            var relationToRemove = await _dbContext.ModelsRelationsTRRT.FirstOrDefaultAsync(trrt => trrt.TruckId.Equals(truckId));

            if (relationToRemove != null)
            {
                _dbContext.ModelsRelationsTRRT.Remove(relationToRemove);
                await _dbContext.SaveChangesAsync();
            }

            return Ok(await _dbContext.ModelsRelationsTRRT.ToListAsync());
        }

        // UPDATE endpoint 
        [HttpPatch("byTruckId/{truckId}")]
        public async Task<IActionResult> PatchByTruckId(Guid truckId, [FromBody] JsonPatchDocument<ModelsRelationTRRT> truckRoutesRelationPatch)
        {
            if (truckRoutesRelationPatch != null)
            {
                var relationToUpdate = await _dbContext.ModelsRelationsTRRT.FirstOrDefaultAsync(trrt => trrt.TruckId.Equals(truckId));

                if (relationToUpdate != null)
                {
                    truckRoutesRelationPatch.ApplyTo(relationToUpdate, ModelState);

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
