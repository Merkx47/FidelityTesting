using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace FidelityTesting;

/// <summary>
/// Lambda entry point that runs your actual ASP.NET Core application
/// with HealthController and WeatherForecastController
/// </summary>
public class LambdaEntryPoint : APIGatewayProxyFunction
{
    /// <summary>
    /// Configures ASP.NET Core to run in Lambda
    /// </summary>
    protected override void Init(IWebHostBuilder builder)
    {
        builder
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .UseLambdaServer();
    }
}

/// <summary>
/// ASP.NET Core startup configuration for Lambda
/// </summary>
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Add your controllers (HealthController, WeatherForecastController)
        services.AddControllers();
        
        // Add API documentation
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Enable Swagger
        app.UseSwagger();
        app.UseSwaggerUI();
        
        // Configure routing
        app.UseRouting();
        app.UseAuthorization();
        
        // Map your controllers
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}