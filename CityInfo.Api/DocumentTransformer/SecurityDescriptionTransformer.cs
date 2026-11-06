using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CityInfo.Api.DocumentTransformer
{
    public class SecurityDescriptionTransformer : IOpenApiDocumentTransformer
    {
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            //document.Components ??= new OpenApiComponents();
            //document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

            ////Add Bearer token security scheme
            //document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
            //{
            //    Type = SecuritySchemeType.Http,
            //    Scheme = "bearer",
            //    BearerFormat = "JWT",
            //    Description = "Input your Bearer token to access this API"
            //});

            ////Apply security requirement globally
            //document.Security = [
            //    new OpenApiSecurityRequirement{
            //        {
            //            new OpenAPiSecuritySchemeReference("Bearer"), []
            //        }
            //    }
            //    ];
            return Task.CompletedTask;

        }
    }
}
