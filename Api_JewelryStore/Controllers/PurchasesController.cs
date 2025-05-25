using Api_JewelryStore.DTO_Models;
using Api_JewelryStore.Models;
using Api_JewelryStore.Models.DTO_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_JewelryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly DiplomDb3Context _context;

        public PurchasesController(DiplomDb3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchases()
        {
            var purchases = await _context.Purchases
                 .Include(p => p.JewelryItemsSize)
        .ThenInclude(js => js.JewelryItem)
            .ThenInclude(ji => ji.ForWho)
    .Include(p => p.JewelryItemsSize)
        .ThenInclude(js => js.JewelryItem)
            .ThenInclude(ji => ji.Category)
    .Include(p => p.JewelryItemsSize)
        .ThenInclude(js => js.JewelryItem)
            .ThenInclude(ji => ji.Material)
            // Добавляем Include для InsertionsDetails + Insertion + InsertionsCharacteristics
    .Include(p => p.JewelryItemsSize)
        .ThenInclude(js => js.JewelryItem)
            .ThenInclude(ji => ji.InsertionsDetails)
                .ThenInclude(id => id.Insertion)
    .Include(p => p.JewelryItemsSize)
        .ThenInclude(js => js.JewelryItem)
            .ThenInclude(ji => ji.InsertionsDetails)
                .ThenInclude(id => id.InsertionsCharacteristics)
    .Select(p => new PurchaseDto
    {
        Id = p.Id,
        UserId = p.UserId,
        Quantity = p.Quantity,
        TotalPrice = p.TotalPrice,
        PurchaseDate = p.PurchaseDate,
        JewelrySize = new JewelrySizeDto
        {
            Id = p.JewelryItemsSize.Id,
            Size = p.JewelryItemsSize.Size,
            StockQuantity = p.JewelryItemsSize.StockQuantity,
            JewelryItem = new JewelryItemDto
            {
                Id = p.JewelryItemsSize.JewelryItem.Id,
                Title = p.JewelryItemsSize.JewelryItem.Title,
                Price = p.JewelryItemsSize.JewelryItem.Price,
                Discount = p.JewelryItemsSize.JewelryItem.Discount,
                PriceDiscounr = p.JewelryItemsSize.JewelryItem.PriceDiscounr,
                Rating = p.JewelryItemsSize.JewelryItem.Rating,
                Articul = p.JewelryItemsSize.JewelryItem.Articul,
                PhotoUrl = p.JewelryItemsSize.JewelryItem.PhotoUrl,
                ApproximateWeight = p.JewelryItemsSize.JewelryItem.ApproximateWeight,

                // Заполняем названия
                Category = new CategoryDto
                {
                    Id = p.JewelryItemsSize.JewelryItem.Category.Id,
                    Name = p.JewelryItemsSize.JewelryItem.Category.Name
                },
                Material = new MaterialDto
                {
                    Id = p.JewelryItemsSize.JewelryItem.Material.Id,
                    Name = p.JewelryItemsSize.JewelryItem.Material.Name
                },
                ForWho = new ForWhoDto
                {
                    Id = p.JewelryItemsSize.JewelryItem.ForWho.Id,
                    Name = p.JewelryItemsSize.JewelryItem.ForWho.Name
                }
            }
        }
    })
    .ToListAsync();

            return Ok(purchases);
        }
    }
}
