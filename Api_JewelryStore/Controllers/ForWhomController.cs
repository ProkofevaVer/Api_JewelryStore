using Api_JewelryStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_JewelryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForWhomController : ControllerBase
    {
        private readonly DiplomDb3Context _context;

        public ForWhomController(DiplomDb3Context context)
        {
            _context = context;
        }
        // GET: api/forWhom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForWho>>> GetMaterials()
        {
            var forWho = await _context.ForWhos.ToListAsync();

            if (forWho == null || !forWho.Any())
            {
                return NotFound("No materials found.");
            }

            return Ok(forWho);
        }
    }
}
