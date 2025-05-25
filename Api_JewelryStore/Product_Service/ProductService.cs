using Api_JewelryStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_JewelryStore.Product_Service
{
    public class ProductService
    {
        private readonly DiplomDb3Context _context;

        public ProductService(DiplomDb3Context context)
        {
            _context = context;
        }

        public async Task<JewelryItem> AddJewelryItemAsync(JewelryItem item)
        {
            // Здесь можно добавить любую бизнес-логику, валидации и т.п.
            // Validate item properties
            var validationErrors = new List<string>();

            // Check if Material exists
            if (!await _context.Materials.AnyAsync(m => m.Id == item.MaterialId))
            {
                validationErrors.Add("Invalid Material ID.");
            }

            // Check if Category exists
            if (!await _context.Categories.AnyAsync(c => c.Id == item.CategoryId))
            {
                validationErrors.Add("Invalid Category ID.");
            }

            // Check if ForWho exists
            if (!await _context.ForWhos.AnyAsync(f => f.Id == item.ForWhoId))
            {
                validationErrors.Add("Invalid ForWho ID.");
            }

            // If there are validation errors, throw an exception
            if (validationErrors.Any())
            {
                throw new ArgumentException(string.Join(" ", validationErrors));
            }
            // Calculate PriceDiscount
            item.PriceDiscounr = item.Price - (item.Price * item.Discount / 100);

            // If there are validation errors, throw an exception
            if (validationErrors.Any())
            {
                throw new ArgumentException(string.Join(" ", validationErrors));
            }

            // Add the item to the context and save changes
            _context.JewelryItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<JewelryItem?> GetJewelryItemByIdAsync(int id)
        {
            return await _context.JewelryItems
                .Include(j => j.Category)
                .Include(j => j.Material)
                .Include(j => j.ForWho)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

    }
}
