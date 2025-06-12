namespace Demo.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public ProductTypeDto Type { get; set; }
        public bool Discontinued { get; set; }
    }
}
