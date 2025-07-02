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
            // Log the ENTIRE request for debugging
            context.Logger.LogLine($"Full Request: {JsonSerializer.Serialize(request, new JsonSerializerOptions { WriteIndented = true })}");
            
            var debugInfo = new
            {
                Path = request.Path,
                RawPath = request.RawPath,
                Resource = request.Resource,
                HttpMethod = request.HttpMethod,
                PathParameters = request.PathParameters,
                QueryStringParameters = request.QueryStringParameters,
                RequestContext = request.RequestContext?.Path,
                Headers = request.Headers
            };

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = JsonSerializer.Serialize(debugInfo, new JsonSerializerOptions { WriteIndented = true }),
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
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