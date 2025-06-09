using Demo.Domain.AggregatesModel.ProductAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository productRepository;

        public ProductsController(ILogger<ProductsController> logger,
            IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<int?> GetAsync()// Product product)
        {

            var product = new Product
            {
                Name = "Sample Product",
                Category = "Sample Category",
                Discontinued = true
            };

            var validator = new ProductValidator();
            var result = validator.Validate(product);

            if (result.IsValid)
            {
                var productAdded = await productRepository.Add(product);

                _logger.LogInformation("Product added: {ProductName}", product.Name);
                _logger.LogInformation("Product id: {ProductId}", productAdded);

                return productAdded;
            }

            return null;
        }
    }
}
