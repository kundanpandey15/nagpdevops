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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMongoDatabase database)
        {
            _logger = logger;
            _weatherForecastCollection = database.GetCollection<WeatherForecast>("WeatherForecast");
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