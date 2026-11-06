using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CityInfo.Api.DocumentTransformer
{
    public class AddSchemaTransformer : IOpenApiSchemaTransformer
    {
        public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
        {
           return Task.CompletedTask;
        }
    }
}
