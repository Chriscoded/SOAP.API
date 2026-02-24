using Microsoft.AspNetCore.Mvc;
using SOAP.Model;

namespace SOAP.Controllers;
[SOAPController(SOAPVersion.v1_2)]
public class Service2Controller : SOAPControllerBase
{
    private static readonly string[] Summaries = new[]
    {
         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<Service2Controller> _logger;
    private readonly IWebHostEnvironment _env;

    public Service2Controller(ILogger<Service2Controller> logger, IWebHostEnvironment env) : base(logger, env)
    {
        _logger = logger;
        _env = env;
    }

    [HttpPost]
    //[Produces("application/xml")]
    public IActionResult Post(SOAP1_2RequestEnvelope env)
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