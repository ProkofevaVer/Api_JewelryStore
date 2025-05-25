using Api_JewelryStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_JewelryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly DiplomDb3Context _context;

        public MaterialController(DiplomDb3Context context)
        {
            _context = context;
        }
        // GET: api/material
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            var materials = await _context.Materials.ToListAsync();

            if (materials == null || !materials.Any())
            {
                return NotFound("No materials found.");
            }

            return Ok(materials);
        }
    }
}