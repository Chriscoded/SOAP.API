using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateBootstrapLogger();

try
{
    Log.Information("Starting Web Host");

    builder.Host.UseSerilog((hostContext,services, configuration) =>
    {
        configuration.ReadFrom.Configuration(builder.Configuration);
    });
    
   // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    //builder.Services.AddOpenApi();
    builder.Services.AddControllers()
        .AddXmlDataContractSerializerFormatters();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        //app.MapOpenApi();
    }

    //app.UseHttpsRedirection();


    app.MapControllers();

    app.Run();

    
}
catch (System.Exception ex)
{
    
    Log.Fatal(ex, "Host terminated unexpectedl");
}
finally
{
    Log.CloseAndFlush();
}

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// } 


