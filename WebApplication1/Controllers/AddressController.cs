using AutoMapper;
using Demo.API.DTOs;
using Demo.Domain.AggregatesModel.Company2Aggregate;
using Demo.Domain.AggregatesModel.CompanyAggregate;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.API.Controllers
{
    /// <summary>
    /// API controller for managing addresses.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddress2Repository addressRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging information.</param>
        /// <param name="addressRepository"></param>
        /// <param name="mapper"></param>
        public AddressController(ILogger<AddressController> logger,
            IAddress2Repository addressRepository,
            IMapper mapper)
        {
            _logger = logger;
            this.addressRepository = addressRepository;
            this._mapper = mapper;  
        }

        /// <summary>
        /// Gets a product from the repository.
        /// </summary>
        /// <param name="id">The product to add.</param>
        /// <returns>The ID of the added product, or null if the operation fails.</returns>
        [SwaggerOperation(Summary = "Endpoint for getting product data from the server.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Add the product to the repository and get the new product's ID.
            var productAdded = await addressRepository.GetById(id);
            if (productAdded == null)
            {
                // Return 404 if the product does not exist.
                return NotFound();
            }
            // Log the name and ID of the added product.
            _logger.LogInformation("Company id: {Id}", productAdded.Id);

            // Return the ID of the added product.
            var product = _mapper.Map<AddressDto>(productAdded);

            return Ok(product);
        }

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The ID of the added product, or null if the operation fails.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Endpoint for posting product data to the server.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] AddressDto dto)
        {
            var product = _mapper.Map<Address2>(dto);

            // Add the product to the repository and get the new product's ID.
            var productAdded = await addressRepository.Add(product);

            // Log the name and ID of the added product.
            _logger.LogInformation("Company added: {Country}", product.Country);
            _logger.LogInformation("Company id: {Id}", productAdded);

            // Return the ID of the added product.
            return Ok(productAdded);
        }

        /// <summary>
        /// Updates an existing company in the repository.
        /// </summary>
        /// <param name="company">The company with updated information.</param>
        /// <returns>No content if successful, or NotFound if the product does not exist.</returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Endpoint for updating product data to the server.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDto dto)
        {
            var company = _mapper.Map<Address2>(dto);

            // Retrieve the existing product by ID.
            var existed = await addressRepository.GetById(company.Id);
            if (existed == null)
            {
                // Return 404 if the product does not exist.
                return NotFound();
            }
            // Update the product in the repository.
            await addressRepository.Update(company);

            // Log the name and ID of the updated product.
            _logger.LogInformation("Company updated: {Country}", company.Country);
            _logger.LogInformation("Company id: {Id}", company.Id);

            // Return 204 No Content to indicate success.
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing company from the repository.
        /// </summary>
        /// <param name="product">The company to delete.</param>
        /// <returns>No content if successful, or NotFound if the company does not exist.</returns>
        [HttpDelete]
        [SwaggerOperation(Summary = "Endpoint for deleting product data from the server.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromBody] AddressDto dto)
        {
            var product = _mapper.Map<Address2>(dto);

            // Retrieve the existing product by ID.
            var existed = await addressRepository.GetById(product.Id);
            if (existed == null)
            {
                // Return 404 if the product does not exist.
                return NotFound();
            }
            // Remove the product from the repository.
            await addressRepository.Remove(product);

            // Log the name and ID of the deleted product.
            _logger.LogInformation("Company removed: {Country}", product.Country);
            _logger.LogInformation("Company id: {Id}", product.Id);

            // Return 204 No Content to indicate success.
            return NoContent();
        }
    }
}