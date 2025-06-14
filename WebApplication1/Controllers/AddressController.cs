using AutoMapper;
using Demo.API.DTOs;
using Demo.Domain.AggregatesModel.Company2Aggregate;
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
        /// <param name="addressRepository">Repository for address data access.</param>
        /// <param name="mapper">Mapper for converting between entities and DTOs.</param>
        public AddressController(ILogger<AddressController> logger,
            IAddress2Repository addressRepository,
            IMapper mapper)
        {
            _logger = logger;
            this.addressRepository = addressRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Gets an address by name from the repository.
        /// </summary>
        /// <param name="name">The name of the address to retrieve.</param>
        /// <returns>The DTO of the address, or NotFound if not found.</returns>
        [SwaggerOperation(Summary = "Endpoint for getting address data by name from the server.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Names/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var address = await addressRepository.GetByName(name);
            if (address == null)
            {
                // Return 404 if the address does not exist.
                return NotFound();
            }
            _logger.LogInformation("Address id: {Id}", address.Id);

            var dto = _mapper.Map<AddressDto>(address);

            return Ok(dto);
        }

        /// <summary>
        /// Gets an address by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the address to retrieve.</param>
        /// <returns>The DTO of the address, or NotFound if not found.</returns>
        [SwaggerOperation(Summary = "Endpoint for getting address data by ID from the server.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await addressRepository.GetById(id);
            if (address == null)
            {
                // Return 404 if the address does not exist.
                return NotFound();
            }
            _logger.LogInformation("Address id: {Id}", address.Id);

            var dto = _mapper.Map<AddressDto>(address);

            return Ok(dto);
        }

        /// <summary>
        /// Adds a new address to the repository.
        /// </summary>
        /// <param name="dto">The address to add.</param>
        /// <returns>The ID of the added address.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Endpoint for posting address data to the server.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] AddressDto dto)
        {
            var address = _mapper.Map<Address2>(dto);

            // Add the address to the repository and get the new address's ID.
            var addressAdded = await addressRepository.Add(address);

            _logger.LogInformation("Address added: {Country}", address.Country);
            _logger.LogInformation("Address id: {Id}", addressAdded);

            return Ok(addressAdded);
        }

        /// <summary>
        /// Updates an existing address in the repository.
        /// </summary>
        /// <param name="dto">The address with updated information.</param>
        /// <returns>No content if successful, or NotFound if the address does not exist.</returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Endpoint for updating address data on the server.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDto dto)
        {
            var address = _mapper.Map<Address2>(dto);

            // Retrieve the existing address by ID.
            var existed = await addressRepository.GetById(address.Id);
            if (existed == null)
            {
                // Return 404 if the address does not exist.
                return NotFound();
            }
            // Update the address in the repository.
            await addressRepository.Update(address);

            _logger.LogInformation("Address updated: {Country}", address.Country);
            _logger.LogInformation("Address id: {Id}", address.Id);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing address from the repository.
        /// </summary>
        /// <param name="dto">The address to delete.</param>
        /// <returns>No content if successful, or NotFound if the address does not exist.</returns>
        [HttpDelete]
        [SwaggerOperation(Summary = "Endpoint for deleting address data from the server.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromBody] AddressDto dto)
        {
            var address = _mapper.Map<Address2>(dto);

            // Retrieve the existing address by ID.
            var existed = await addressRepository.GetById(address.Id);
            if (existed == null)
            {
                // Return 404 if the address does not exist.
                return NotFound();
            }
            // Remove the address from the repository.
            await addressRepository.Remove(address);

            _logger.LogInformation("Address removed: {Country}", address.Country);
            _logger.LogInformation("Address id: {Id}", address.Id);

            return NoContent();
        }
    }
}