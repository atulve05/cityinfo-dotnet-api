using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.OpenApi;

namespace CityInfo.Api.DocumentTransformer
{
    public class AddResponseDescriptionOperationTransformer : IOpenApiOperationTransformer
    {
        public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
        {
            if (operation.Responses is null)
                return Task.CompletedTask;

            foreach (var (statusCode, response) in operation.Responses)
            {
                if (IsDefaultOrEmptyDescription(statusCode, response.Description))
                {
                    response.Description = statusCode switch
                    {
                        "200" => "Success - the resource was returned",
                        "201" => "Created - the resource was created. " +
                                "The location header contain the URI" +
                                "of the new resource",
                        _ => response.Description,

                    };
                }
            }
            return Task.CompletedTask;
        }

        private static bool IsDefaultOrEmptyDescription(string statusCode, string? description)
        {
            if (string.IsNullOrEmpty(description))
                return true;
            if (!int.TryParse(statusCode, out var code))
                return false;

            var defaultDescription = ReasonPhrases.GetReasonPhrase(code);
            return string.Equals(description, defaultDescription, StringComparison.OrdinalIgnoreCase);
        }
    }
}
