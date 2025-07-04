// LambdaEntryPoint.cs (ONLY file we needed to add)
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace FidelityTesting;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureServices(services =>
            {
                // Mirror your original Program.cs services
                services.AddControllers();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
            })
            .Configure(app =>
            {
                // Mirror your original Program.cs pipeline
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseRouting();
                app.UseAuthorization();
                app.UseEndpoints(endpoints => endpoints.MapControllers());
            })
            .UseLambdaServer();
    }
}


















// using Amazon.Lambda.AspNetCoreServer;
// using Amazon.Lambda.Core;
// using Amazon.Lambda.Serialization.SystemTextJson;

// [assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

// namespace FidelityTesting;

// public class LambdaEntryPoint : APIGatewayProxyFunction
// {
//     protected override void Init(IWebHostBuilder builder)
//     {
//         builder
//             .UseContentRoot(Directory.GetCurrentDirectory())
//             .UseStartup<Startup>()
//             .UseLambdaServer();
//     }
// }

// public class Startup
// {
//     public void ConfigureServices(IServiceCollection services)
//     {
//         services.AddControllers();
//         services.AddEndpointsApiExplorer();
//         services.AddSwaggerGen();
//     }

//     public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//     {
//         app.UseSwagger();
//         app.UseSwaggerUI();
//         app.UseRouting();
//         app.UseAuthorization();
//         app.UseEndpoints(endpoints =>
//         {
//             endpoints.MapControllers();
//         });
//     }
// }