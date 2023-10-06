using Microsoft.AspNetCore.Mvc;

namespace HeaderRequestAPI.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        public IActionResult Get()
        {
        //For all headers
        //Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
        //foreach (var header in Request.Headers)
        //{
        //    requestHeaders.Add(header.Key, header.Value);
        //}

        //https://www.infoworld.com/article/3617984/how-to-read-request-headers-in-aspnet-core-5-mvc.html#:~:text=Using%20the%20%5BFromQuery%5D%20and%20%5B,your%20controller%20using%20request%20headers.

            Request.Headers.TryGetValue("country", out var headerValue);

            string? country = headerValue.FirstOrDefault();

            if (country != null)
            {
                if (country == "Italy")
                {
                    return Ok(country);
                }
                else
                {
                    return BadRequest("Incorrect Value for country");
                }
            }

            var res = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(res);
        }
    }
}