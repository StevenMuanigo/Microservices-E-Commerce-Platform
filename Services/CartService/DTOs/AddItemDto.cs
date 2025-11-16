namespace CartService.DTOs
{
    public class AddItemDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageFile { get; set; } = string.Empty;
    }
}