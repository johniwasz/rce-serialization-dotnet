# Introduction

This project demonstrates serialization vulnerabilities using Newtonsoft.Json and BinarySerialization. The Todo project is baseed on the starter tutorial available here:

[Create a controller based API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio)

These examples were assembled based on serialization vulnerabilities highlighted in the [ysoserial.net](https://github.com/pwntester/ysoserial.net) git repo.

# Vulnerabilies

## JSON Vunlnerability

The JSON vulnerability is exposed in Newtonsoft.Json through the use of `TypeNameResolver.`

With .NET 6+ it is not possible to override the default JSON serializer from System.Text.Json when using minimal APIs. See [Minimal APIs quick reference](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#configure-json-deserialization-options-for-body-binding).

Following this tutorial:


``` C#
using Newtonsoft.Json;
using rce_serialization_dotnet;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/weather",  () =>
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = WeatherForecast.Summaries[Random.Shared.Next(WeatherForecast.Summaries.Length)]
        })
               .ToArray();
    });

app.MapPost("/storage", async (HttpContext context) => 
  {

    if (!context.Request.HasJsonContentType())
    {
        throw new BadHttpRequestException(
            "Request content type was not a recognized JSON content type.",
            StatusCodes.Status415UnsupportedMediaType);
    }

    using var sr = new StreamReader(context.Request.Body);
    var str = await sr.ReadToEndAsync();

    object? storageObject = JsonConvert.DeserializeObject(str, new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto
    });


});


app.Run();
```