namespace Api_JewelryStore.Models
{
    public class AddCartItemDto
    {
        public int JewelrySizesItemId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public int CardQuantity { get; set; }
        public decimal CardTotalPrice { get; set; }
        public DateOnly CardDate { get; set; }
    }
}
