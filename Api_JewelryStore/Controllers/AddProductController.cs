using Api_JewelryStore.Models.DTO_Models;
using Api_JewelryStore.Models;
using Api_JewelryStore.Product_Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_JewelryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public AddProductController( ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<JewelryItemDto>> AddJewelryItem([FromBody] AddJewelryItemDto newItemDto)
        {
            // Преобразование DTO в модель JewelryItem
            var newItem = new JewelryItem
            {
                Title = newItemDto.Title,
                Price = newItemDto.Price,
                Discount = newItemDto.Discount,
                PriceDiscounr = newItemDto.PriceDiscounr,
                Rating = newItemDto.Rating,
                Articul = newItemDto.Articul,
                PhotoUrl = newItemDto.PhotoUrl,
                CategoryId = newItemDto.CategoryId,
                MaterialId = newItemDto.MaterialId,
                ForWhoId = newItemDto.ForWhoId,
                StockQuantity = newItemDto.StockQuantity,
                ApproximateWeight = newItemDto.ApproximateWeight
            };

            var addedItem = await _productService.AddJewelryItemAsync(newItem);

            // Преобразование добавленной модели в DTO для возврата
            var addedItemDto = new JewelryItemDto
            {
                Id = addedItem.Id,
                Title = addedItem.Title,
                Price = addedItem.Price,
                Discount = addedItem.Discount,
                PriceDiscounr = addedItem.PriceDiscounr,
                Rating = addedItem.Rating,
                Articul = addedItem.Articul,
                PhotoUrl = addedItem.PhotoUrl,
                ApproximateWeight = addedItem.ApproximateWeight,
                Category = new CategoryDto
                {
                    Id = addedItem.Category?.Id ?? 0,
                    Name = addedItem.Category?.Name
                },
                Material = new MaterialDto
                {
                    Id = addedItem.Material?.Id ?? 0,
                    Name = addedItem.Material?.Name
                },
                ForWho = new ForWhoDto
                {
                    Id = addedItem.ForWho?.Id ?? 0,
                    Name = addedItem.ForWho?.Name
                }
            };

            return CreatedAtAction(nameof(GetJewelryItemById), new { id = addedItemDto.Id }, addedItemDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<JewelryItemDtoProd>> GetJewelryItemById(int id)
        {
            var item = await _productService.GetJewelryItemByIdAsync(id);
            if (item == null) return NotFound();

            // Преобразование модели в DTO
            var itemDto = new JewelryItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Price = item.Price,
                Discount = item.Discount,
                PriceDiscounr = item.PriceDiscounr,
                Rating = item.Rating,
                Articul = item.Articul,
                PhotoUrl = item.PhotoUrl,
                ApproximateWeight = item.ApproximateWeight,
                Category = new CategoryDto
                {
                    Id = item.Category?.Id ?? 0,
                    Name = item.Category?.Name
                },
                Material = new MaterialDto
                {
                    Id = item.Material?.Id ?? 0,
                    Name = item.Material?.Name
                },
                ForWho = new ForWhoDto
                {
                    Id = item.ForWho?.Id ?? 0,
                    Name = item.ForWho?.Name
                }
            };

            return Ok(itemDto);
        }

    }
}
