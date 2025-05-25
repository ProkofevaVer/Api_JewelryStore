using Api_JewelryStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_JewelryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DiplomDb3Context _context;

        public CategoryController(DiplomDb3Context context)
        {
            _context = context;
        }
        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetMaterials()
        {
            var category = await _context.Categories.ToListAsync();

            if (category == null || !category.Any())
            {
                return NotFound("No materials found.");
            }

            return Ok(category);
        }
    }
}
