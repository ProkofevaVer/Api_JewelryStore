namespace Api_JewelryStore.Models.DTO_Models
{
    public class JewelrySizeDto
    {
        public int Id { get; set; }

        public int? JewelryItemId { get; set; }

        public int? Size { get; set; }

        public int? StockQuantity { get; set; }

        public JewelryItemDto? JewelryItem { get; set; } // Информация о ювелирном изделии
    }
}
