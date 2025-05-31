using Microsoft.AspNetCore.Mvc;
using NHibernateTestBlog;
using WebApplication1.Domain;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IProductRepository productRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<int> GetAsync()
        {
            var product = new Product
            {
                Name = "Sample Product",
                Category = "Sample Category",
                Discontinued = false
            };

            var result = await productRepository.Add(product);

            _logger.LogInformation("Product added: {ProductName}", product.Name);

            return result;
        }
    }
}
