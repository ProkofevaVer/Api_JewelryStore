using Api_JewelryStore.Models.DTO_Models;
using Api_JewelryStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_JewelryStore.Product_Service;

namespace Api_JewelryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DiplomDb3Context _context;
        public ProductController(DiplomDb3Context context)
        {
            _context = context;
            
        }     

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JewelryItemDto>>> GetProducts()
        {
            var products = await _context.JewelryItems
                .Include(j => j.InsertionsDetails)
                    .ThenInclude(d => d.Insertion)
                .Include(j => j.InsertionsDetails)
                    .ThenInclude(d => d.InsertionsCharacteristics)
                .Include(j => j.Material)
                .Include(j => j.ForWho)
                .Include(j => j.Category)
                .Include(j => j.JewelrySizes)
                .ToListAsync();

            //Преобразуем модели в DTO
            var productDtos = products.Select(p => new JewelryItemDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Discount = p.Discount,
                PriceDiscounr = p.PriceDiscounr,
                Rating = p.Rating,
                Articul = p.Articul,
                PhotoUrl = p.PhotoUrl,
                ApproximateWeight = p.ApproximateWeight,
                Material = new MaterialDto
                {
                    Id = p.Material?.Id ?? 0,
                    Name = p.Material?.Name
                },
                ForWho = new ForWhoDto
                {
                    Id = p.ForWho?.Id ?? 0,
                    Name = p.ForWho?.Name
                },
                Category = new CategoryDto // Добавляем информацию о категории
                {
                    Id = p.Category?.Id ?? 0,
                    Name = p.Category?.Name
                },
                InsertionsDetails = p.InsertionsDetails.Select(d => new InsertionsDetailDto
                {
                    Id = d.Id,
                    Quantity = d.Quantity,
                    Insertion = new InsertionDto
                    {
                        Id = d.Insertion?.Id ?? 0,
                        Name = d.Insertion?.Name
                    },
                    InsertionsCharacteristics = d.InsertionsCharacteristics.Select(ic => new InsertionsCharacteristicDto
                    {
                        Id = ic.Id,
                        ShapeForm = ic.ShapeForm,
                        Color = ic.Color,
                        WeightCarat = ic.WeightCarat,
                        Dimensions = ic.Dimensions,
                        Clarity = ic.Clarity,
                        CutOgranka = ic.CutOgranka,
                        ColorGrade = ic.ColorGrade
                    }).ToList()
                }).ToList(),
                JewelrySizes = p.JewelrySizes.Select(js => new JewelrySizeDto
                {
                    Id = js.Id,
                    JewelryItemId = js.JewelryItemId,
                    Size = js.Size,
                    StockQuantity = js.StockQuantity
                }).ToList() // Добавляем размеры
            }).ToList();

            return Ok(productDtos);
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<JewelryItemDto>>> SearchProducts([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Параметр title не может быть пустым.");
            }

            // Разбиваем строку на отдельные слова и приводим к нижнему регистру
            var searchTerms = title.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Получаем все продукты и фильтруем по каждому из поисковых терминов
            var products = await _context.JewelryItems
                .Include(j => j.InsertionsDetails)
                    .ThenInclude(d => d.Insertion)
                .Include(j => j.InsertionsDetails)
                    .ThenInclude(d => d.InsertionsCharacteristics)
                .Include(j => j.Material)
                .Include(j => j.ForWho)
                .Include(j => j.Category)
                .Include(j => j.JewelrySizes)
                .Where(p => searchTerms.All(term => p.Title.ToLower().Contains(term)))
                .ToListAsync();

            var productDtos = products.Select(p => new JewelryItemDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Discount = p.Discount,
                PriceDiscounr = p.PriceDiscounr,
                Rating = p.Rating,
                Articul = p.Articul,
                PhotoUrl = p.PhotoUrl,
                ApproximateWeight = p.ApproximateWeight,
                Material = new MaterialDto
                {
                    Id = p.Material?.Id ?? 0,
                    Name = p.Material?.Name
                },
                ForWho = new ForWhoDto
                {
                    Id = p.ForWho?.Id ?? 0,
                    Name = p.ForWho?.Name
                },
                Category = new CategoryDto
                {
                    Id = p.Category?.Id ?? 0,
                    Name = p.Category?.Name
                },
                InsertionsDetails = p.InsertionsDetails.Select(d => new InsertionsDetailDto
                {
                    Id = d.Id,
                    Quantity = d.Quantity,
                    Insertion = new InsertionDto
                    {
                        Id = d.Insertion?.Id ?? 0,
                        Name = d.Insertion?.Name
                    },
                    InsertionsCharacteristics = d.InsertionsCharacteristics.Select(ic => new InsertionsCharacteristicDto
                    {
                        Id = ic.Id,
                        ShapeForm = ic.ShapeForm,
                        Color = ic.Color,
                        WeightCarat = ic.WeightCarat,
                        Dimensions = ic.Dimensions,
                        Clarity = ic.Clarity,
                        CutOgranka = ic.CutOgranka,
                        ColorGrade = ic.ColorGrade
                    }).ToList()
                }).ToList(),
                JewelrySizes = p.JewelrySizes.Select(js => new JewelrySizeDto
                {
                    Id = js.Id,
                    JewelryItemId = js.JewelryItemId,
                    Size = js.Size,
                    StockQuantity = js.StockQuantity
                }).ToList()
            }).ToList();

            return Ok(productDtos);
        }


    }

}
