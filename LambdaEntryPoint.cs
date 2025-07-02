using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FidelityTesting;

public class LambdaEntryPoint
{
    public APIGatewayProxyResponse FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        try
        {
            context.Logger.LogLine($"Request: {JsonSerializer.Serialize(request)}");
            
            // Handle health endpoint
            if (request.Path?.Contains("/health") == true)
            {
                var healthResponse = new
                {
                    Status = "Healthy",
                    Timestamp = DateTime.UtcNow,
                    Message = "Lambda is working!",
                    Path = request.Path,
                    Method = request.HttpMethod
                };

                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = JsonSerializer.Serialize(healthResponse),
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        { "Access-Control-Allow-Origin", "*" }
                    }
                };
            }

            // Default response
            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = "Hello from Lambda! Path: " + request.Path,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "text/plain" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };
        }
        catch (Exception ex)
        {
            context.Logger.LogLine($"Error: {ex}");
            return new APIGatewayProxyResponse
            {
                StatusCode = 500,
                Body = $"Error: {ex.Message}",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }
    }
}