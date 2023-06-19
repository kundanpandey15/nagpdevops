using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace climateapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMongoCollection<WeatherForecast> _weatherForecastCollection;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMongoDatabase database, IConfiguration configuration)
        {
            _logger = logger;
            _weatherForecastCollection = database.GetCollection<WeatherForecast>("WeatherForecast");
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = _weatherForecastCollection.Find(_ => true).ToList();
            return result;
        }

        [HttpGet]
        [Route("test")]
        public string GetTestPage()
        {
            return "Test Page hit is successfull";
        }

        [HttpGet]
        [Route("test1")]
        public string GetTestPage1()
        {
            var userName = _configuration.GetSection("username").Value ?? "";
            var password = _configuration.GetSection("password").Value ?? "";
            var connectionString = String.Format(_configuration.GetSection("ConnectionString").Value ?? "", userName, password);

            return connectionString;
        }

        [HttpPost]
        public IActionResult SaveWeatherForecast([FromBody] WeatherForecast weatherForecast)
        {
            try
            {
                _weatherForecastCollection.InsertOne(weatherForecast);
                return Ok("Weather forecast saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving the weather forecast: {ex.Message}");
            }
        }
    }
}