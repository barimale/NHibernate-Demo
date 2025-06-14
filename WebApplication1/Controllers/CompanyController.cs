using AutoMapper;
using Demo.API.DTOs;
using Demo.Domain.AggregatesModel.Company2Aggregate;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.API.Controllers
{
    /// <summary>
    /// API controller for managing companies.
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
        /// <param name="productRepository">Repository for company operations.</param>
        /// <param name="addressRepository">Repository for address operations.</param>
        /// <param name="mapper">Mapper for converting between entities and DTOs.</param>
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
        /// Gets a company by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the company to retrieve.</param>
        /// <returns>The DTO of the company, or NotFound if the company does not exist.</returns>
        [SwaggerOperation(Summary = "Endpoint for getting company data from the server.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await companyRepository.GetById(id);
            if (company == null)
            {
                // Return 404 if the company does not exist.
                return NotFound();
            }
            _logger.LogInformation("Company id: {Id}", company.Id);

            var dto = _mapper.Map<CompanyDto>(company);

            return Ok(dto);
        }

        /// <summary>
        /// Adds a new company to the repository.
        /// </summary>
        /// <param name="dto">The company to add.</param>
        /// <returns>The ID of the added company.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Endpoint for posting company data to the server.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] CompanyDto dto)
        {
            var company = _mapper.Map<Company2>(dto);

            // Add the company to the repository and get the new company's ID.
            var companyAdded = await companyRepository.Add(company);

            _logger.LogInformation("Company added: {Name}", company.Foo);
            _logger.LogInformation("Company id: {Id}", companyAdded);

            return Ok(companyAdded);
        }

        /// <summary>
        /// Updates an existing company in the repository.
        /// </summary>
        /// <param name="dto">The company with updated information.</param>
        /// <returns>No content if successful, or NotFound if the company does not exist.</returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Endpoint for updating company data on the server.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromBody] CompanyDto dto)
        {
            var company = _mapper.Map<Company2>(dto);

            // Retrieve the existing company by ID.
            var existed = await companyRepository.GetById(company.Id);
            if (existed == null)
            {
                // Return 404 if the company does not exist.
                return NotFound();
            }
            // Update the company in the repository.
            await companyRepository.Update(company);

            _logger.LogInformation("Company updated: {Name}", company.Foo);
            _logger.LogInformation("Company id: {Id}", company.Id);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing company from the repository.
        /// </summary>
        /// <param name="dto">The company to delete.</param>
        /// <returns>No content if successful, or NotFound if the company does not exist.</returns>
        [HttpDelete]
        [SwaggerOperation(Summary = "Endpoint for deleting company data from the server.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromBody] CompanyDto dto)
        {
            var company = _mapper.Map<Company2>(dto);

            // Retrieve the existing company by ID.
            var existed = await companyRepository.GetById(company.Id);
            if (existed == null)
            {
                // Return 404 if the company does not exist.
                return NotFound();
            }
            // Remove the company from the repository.
            await companyRepository.Remove(company);

            _logger.LogInformation("Company removed: {Name}", company.Foo);
            _logger.LogInformation("Company id: {Id}", company.Id);

            return NoContent();
        }

        /// <summary>
        /// Associates an existing address with a company.
        /// </summary>
        /// <param name="companyId">The ID of the company to associate the address with.</param>
        /// <param name="addressId">The ID of the address to associate.</param>
        /// <returns>The ID of the updated company if successful, or NotFound if either entity does not exist.</returns>
        [HttpPost("{companyId}/Address/{addressId}")]
        [SwaggerOperation(Summary = "Endpoint for adding an address to a company.")]
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

            _logger.LogInformation("Address Id assigned to Company: {Id}", address.Id);
            _logger.LogInformation("Company id: {Id}", company.Id);

            // Return the updated company's ID.
            return Ok(result?.Id);
        }
    }
}