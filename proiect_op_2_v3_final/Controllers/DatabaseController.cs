using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;

namespace proiect_op_2_v3_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DatabaseController : ControllerBase
    {
        private readonly tableContext _tableContext;

        public DatabaseController(tableContext tablecontext)
        {
            _tableContext = tablecontext;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _tableContext.Cities.ToListAsync());
        }
    }
}