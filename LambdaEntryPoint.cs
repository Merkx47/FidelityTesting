using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FidelityTesting;

public class LambdaEntryPoint
{
    public APIGatewayProxyResponse FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        try
        {
            // Simple debug without complex JSON serialization
            var debugText = $@"
Path: '{request.Path}'
RawPath: '{request.RawPath}'
Resource: '{request.Resource}'
HttpMethod: '{request.HttpMethod}'
RequestContext.Path: '{request.RequestContext?.Path}'
PathParameters: {(request.PathParameters?.Count ?? 0)} items
Headers: {(request.Headers?.Count ?? 0)} items
";

            context.Logger.LogLine($"Request Debug: {debugText}");

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = debugText,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "text/plain" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };
        }
        catch (Exception ex)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 500,
                Body = $"Error: {ex.Message}",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }
    }
}