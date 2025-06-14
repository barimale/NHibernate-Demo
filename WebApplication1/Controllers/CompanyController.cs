using AutoMapper;
using Demo.API.DTOs;
using Demo.Domain.AggregatesModel.Company2Aggregate;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.API.Controllers
{
    /// <summary>
    /// API controller for managing products.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompany2Repository companyRepository;
        private readonly IAddress2Repository addressRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging information.</param>
        /// <param name="productRepository">Repository for product operations.</param>
        /// <param name="addressRepository"></param>
        /// <param name="mapper"></param>
        public CompanyController(ILogger<CompanyController> logger,
            ICompany2Repository productRepository,
            IAddress2Repository addressRepository,
            IMapper mapper)
        {
            _logger = logger;
            this.companyRepository = productRepository;
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
            var productAdded = await companyRepository.GetById(id);
            if (productAdded == null)
            {
                // Return 404 if the product does not exist.
                return NotFound();
            }
            // Log the name and ID of the added product.
            _logger.LogInformation("Company id: {Id}", productAdded.Id);

            // Return the ID of the added product.
            var product = _mapper.Map<CompanyDto>(productAdded);

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
        public async Task<IActionResult> AddAsync([FromBody] CompanyDto dto)
        {
            var product = _mapper.Map<Company2>(dto);

            // Add the product to the repository and get the new product's ID.
            var productAdded = await companyRepository.Add(product);

            // Log the name and ID of the added product.
            _logger.LogInformation("Company added: {Name}", product.Foo);
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
        public async Task<IActionResult> UpdateAsync([FromBody] CompanyDto dto)
        {
            var company = _mapper.Map<Company2>(dto);

            // Retrieve the existing product by ID.
            var existed = await companyRepository.GetById(company.Id);
            if (existed == null)
            {
                // Return 404 if the product does not exist.
                return NotFound();
            }
            // Update the product in the repository.
            await companyRepository.Update(company);

            // Log the name and ID of the updated product.
            _logger.LogInformation("Company updated: {Name}", company.Foo);
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
        public async Task<IActionResult> DeleteAsync([FromBody] CompanyDto dto)
        {
            var product = _mapper.Map<Company2>(dto);

            // Retrieve the existing product by ID.
            var existed = await companyRepository.GetById(product.Id);
            if (existed == null)
            {
                // Return 404 if the product does not exist.
                return NotFound();
            }
            // Remove the product from the repository.
            await companyRepository.Remove(product);

            // Log the name and ID of the deleted product.
            _logger.LogInformation("Company removed: {Name}", product.Foo);
            _logger.LogInformation("Company id: {Id}", product.Id);

            // Return 204 No Content to indicate success.
            return NoContent();
        }

        /// <summary>
        /// Associates an existing address with a company.
        /// </summary>
        /// <param name="companyId">The ID of the company to associate the address with.</param>
        /// <param name="addressId">The ID of the address to associate.</param>
        /// <returns>Returns the updated company if successful, or NotFound if either entity does not exist.</returns>
        [HttpPost("{companyId}/add-address/{addressId}")]
        [SwaggerOperation(Summary = "Endpoint for adding address to company.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAddressToCompany(int companyId, int addressId)
        {
            // Retrieve the company by its ID.
            var company = await companyRepository.GetById(companyId);
            // Retrieve the address by its ID.
            var address = await addressRepository.GetById(addressId);

            // If either the company or address does not exist, return 404.
            if (company == null || address == null)
            {
                return NotFound("Company or Address not found.");
            }

            // Associate the address with the company.
            var result = await addressRepository.AssignAddressToCompany(addressId, companyId);

            // Log the name and ID of the deleted product.
            _logger.LogInformation("Address Id asign to Company : {Id}", address.Id);
            _logger.LogInformation("Company id: {Id}", company.Id);

            // Return the updated company.
            return Ok(result?.Id);
        }
    }
}