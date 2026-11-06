using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CityInfo.Api.DocumentTransformer
{
    public class AddGeneralInformationTransformer : IOpenApiDocumentTransformer
    {
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            document.Info = new()
            {
                Title = "Web API",
                Version = context.DocumentName,
                Description = "DEScrip",
                Contact = new()
                {
                    Name = "Atul",
                    Email = "Avgmail"
                }
            };
            return Task.CompletedTask;
        }
    }
}
