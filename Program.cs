namespace FidelityTesting;

/// <summary>
/// Main program entry point for local development and testing.
/// When deployed to Lambda, the LambdaEntryPoint class is used instead.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method - entry point for local development.
    /// This runs when you use 'dotnet run' locally, but not in Lambda.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Creates and configures the web host builder for local development.
    /// This configuration mirrors what's in the Startup class for consistency.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>Configured host builder</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    // Add controller services
                    services.AddControllers();
                    
                    // Add API documentation services
                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen();
                });

                webBuilder.Configure((context, app) =>
                {
                    // Always enable Swagger for local development
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
                });
            });
}























// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// app.UseSwagger();
// app.UseSwaggerUI();
// // Configure the HTTP request pipeline.
// //if (app.Environment.IsDevelopment())
// //{
// //    app.UseSwagger();
// //    app.UseSwaggerUI();
// //}

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();
