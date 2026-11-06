using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CityInfo.Api.DocumentTransformer
{
    public class RemoveInternalOperationsTransformer(IHostEnvironment environment) : IOpenApiDocumentTransformer
    {
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            if (!environment.IsProduction())
                return Task.CompletedTask;

            //Remove operations tagged as "Internal"
            foreach (var path in document.Paths.Values)
            {
                var operationsToRemove = path.Operations?
                        .Where(op => op.Value.Tags?.Any(tag => tag.Name == "Internal") == true)
                        .Select(op => op.Key)
                        .ToList() ?? [];

                foreach (var operationType in operationsToRemove)
                {
                    path.Operations?.Remove(operationType);
                }
            }

            //Remove paths that have no operations left;

            return Task.CompletedTask;
        }
    }
}
