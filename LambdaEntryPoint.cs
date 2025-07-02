using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;

// This tells Lambda to use System.Text.Json for serialization
[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace FidelityTesting;

/// <summary>
/// Lambda entry point for the FidelityTesting application.
/// This class handles the integration between AWS Lambda and ASP.NET Core.
/// </summary>
public class LambdaEntryPoint : APIGatewayProxyFunction
{
    /// <summary>
    /// Initializes the ASP.NET Core application for Lambda execution.
    /// This method is called once when the Lambda function is initialized.
    /// </summary>
    /// <param name="builder">The web host builder to configure</param>
    protected override void Init(IWebHostBuilder builder)
    {
        builder
            // Use the current directory as the content root
            .UseContentRoot(Directory.GetCurrentDirectory())
            // Use our Program class for configuration (no Startup.cs needed)
            .UseStartup<Startup>()
            // Enable Lambda server integration
            .UseLambdaServer();
    }
}

/// <summary>
/// Startup class for Lambda execution.
/// This replaces the inline configuration in Program.cs when running in Lambda.
/// </summary>
public class Startup
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the Startup class.
    /// </summary>
    /// <param name="configuration">The application configuration</param>
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Configures services for dependency injection.
    /// This method gets called by the runtime to add services to the container.
    /// </summary>
    /// <param name="services">The service collection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        // Add controller services
        services.AddControllers();
        
        // Add API documentation services
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    /// <summary>
    /// Configures the HTTP request pipeline.
    /// This method gets called by the runtime to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The application builder</param>
    /// <param name="env">The web host environment</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Always enable Swagger for Lambda (helps with testing)
        app.UseSwagger();
        app.UseSwaggerUI();

        // Configure the HTTP request pipeline
        app.UseRouting();
        app.UseAuthorization();

        // Map controller routes
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}