using Microsoft.AspNetCore.Mvc;
using NHibernateTestBlog;
using WebApplication1.Domain;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IProductRepository productRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            var product = new Product
            {
                Name = "Sample Product",
                Category = "Sample Category",
                Discontinued = false
            };

            await productRepository.Add(product);

            _logger.LogInformation("Product added: {ProductName}", product.Name);


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
