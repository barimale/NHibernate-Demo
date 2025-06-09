using Demo.Domain.AggregatesModel.ProductAggregate;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.API.Controllers
{
    /// <summary>
    /// API controller for managing products.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging information.</param>
        /// <param name="productRepository">Repository for product operations.</param>
        public ProductsController(ILogger<ProductsController> logger,
            IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The ID of the added product, or null if the operation fails.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Endpoint for putting product data to the server.")]
        public async Task<int?> PostAsync([FromBody] Product product)
        {
            // Add the product to the repository.
            var productAdded = await productRepository.Add(product);

            // Log product addition details.
            _logger.LogInformation("Product added: {ProductName}", product.Name);
            _logger.LogInformation("Product id: {ProductId}", productAdded);

            return productAdded;
        }
    }
}