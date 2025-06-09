using Demo.Domain.AggregatesModel.ProductAggregate;
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

        [HttpPost]
        public async Task<int?> PostAsync([FromBody] Product product)
        {

            var productAdded = await productRepository.Add(product);

            _logger.LogInformation("Product added: {ProductName}", product.Name);
            _logger.LogInformation("Product id: {ProductId}", productAdded);

            return productAdded;
        }
    }
}
