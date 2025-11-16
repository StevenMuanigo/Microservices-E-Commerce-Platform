namespace CatalogService.DTOs
{
    public class UpdateProductDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageFile { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}