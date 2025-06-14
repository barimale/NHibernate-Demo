using AutoMapper;
using Demo.API.DTOs;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging information.</param>
        /// <param name="productRepository">Repository for product operations.</param>
        /// <param name="mapper">Mapper for converting between entities and DTOs.</param>
        public ProductsController(ILogger<ProductsController> logger,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _logger = logger;
            this.productRepository = productRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Gets a product by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The DTO of the product, or NotFound if the product does not exist.</returns>
        [SwaggerOperation(Summary = "Endpoint for getting product data by ID from the server.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await productRepository.GetById(id);
            if (product == null)
            {
                // Return 404 if the product does not exist.
                return NotFound();
            }
            _logger.LogInformation("Product id: {Id}", product.Id);

            var dto = _mapper.Map<ProductDto>(product);

            return Ok(dto);
        }

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="dto">The product to add.</param>
        /// <returns>The ID of the added product.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Endpoint for posting product data to the server.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            // Add the product to the repository and get the new product's ID.
            var productAdded = await productRepository.Add(product);

            _logger.LogInformation("Product added: {ProductName}", product.Name);
            _logger.LogInformation("Product id: {ProductId}", productAdded);

            return Ok(productAdded);
        }

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="dto">The product with updated information.</param>
        /// <returns>No content if successful, or NotFound if the product does not exist.</returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Endpoint for updating product data on the server.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            // Retrieve the existing product by ID.
            var existed = await productRepository.GetById(product.Id);
            if (existed == null)
            {
                // Return 404 if the product does not exist.
                return NotFound();
            }
            // Update the product in the repository.
            await productRepository.Update(product);

            _logger.LogInformation("Product updated: {ProductName}", product.Name);
            _logger.LogInformation("Product id: {ProductId}", product.Id);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing product from the repository.
        /// </summary>
        /// <param name="dto">The product to delete.</param>
        /// <returns>No content if successful, or NotFound if the product does not exist.</returns>
        [HttpDelete]
        [SwaggerOperation(Summary = "Endpoint for deleting product data from the server.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromBody] ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            // Retrieve the existing product by ID.
            var existed = await productRepository.GetById(product.Id);
            if (existed == null)
            {
                // Return 404 if the product does not exist.
                return NotFound();
            }
            // Remove the product from the repository.
            await productRepository.Remove(product);

            _logger.LogInformation("Product removed: {ProductName}", product.Name);
            _logger.LogInformation("Product id: {ProductId}", product.Id);

            return NoContent();
        }
    }
}