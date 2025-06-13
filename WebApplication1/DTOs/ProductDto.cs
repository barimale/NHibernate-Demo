namespace Demo.API.DTOs
{
    public class ProductDto : EntityDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public ProductTypeDto Type { get; set; }
        public bool Discontinued { get; set; }
    }
}
