using Api_JewelryStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_JewelryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsertionController : ControllerBase
    {
        private readonly DiplomDb3Context _context;

        public InsertionController(DiplomDb3Context context)
        {
            _context = context;
        }
        // GET: api/insertions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForWho>>> GetMaterials()
        {
            var insertions = await _context.Insertions.ToListAsync();

            if (insertions == null || !insertions.Any())
            {
                return NotFound("No materials found.");
            }

            return Ok(insertions);
        }
    }
}
