using Api_JewelryStore.Models.DTO_Models;

namespace Api_JewelryStore.DTO_Models
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateOnly? PurchaseDate { get; set; }
        public JewelrySizeDto JewelrySize { get; set; }
    }
}
