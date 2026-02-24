using Microsoft.AspNetCore.Mvc;
using SOAP.Model;

namespace SOAP.Controllers;
[SOAPController(SOAPVersion.v1_1)]
public class Service1Controller : SOAPControllerBase
{
    private static readonly string[] Summaries = new[]
    {
         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<Service1Controller> _logger;
    private readonly IWebHostEnvironment _env;

    public Service1Controller(ILogger<Service1Controller> logger, IWebHostEnvironment env) : base(logger, env)
    {
        _logger = logger;
        _env = env;
    }

    [HttpPost]
    //[Produces("application/xml")]
    public IActionResult Post(SOAP1_1RequestEnvelope env)
    {
        _logger.LogInformation("Got in the controller");

        //var res = new SOAPResponseEnvelope();
         var res = CreateSOAPResponseEnvelope();
        res.Body.GetWeatherForecastResponse = new GetWeatherForecastResponse()
        {
             WeatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray()
        };
        return Ok(res);
    }
}