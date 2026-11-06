using System.Text;
using Asp.Versioning;
using CityInfo.Domain;
using CityInfo.Data.Services;
using CityInfo.Data.DbContexts;
using CityInfo.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CityInfo.Data.Services.Interface;
using Scalar.AspNetCore;
using CityInfo.Api.DocumentTransformer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Media formators// client sent Accept header which api not suppported, return Not Acceptable
    options.ReturnHttpNotAcceptable = true;
}) //.AddNewtonsoftJson()  //replace default json I/O format to json.net
.AddXmlDataContractSerializerFormatters();

// .NET 9's built-in OpenAPI generator needs transformers to include XML comments.
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<AddGeneralInformationTransformer>();
    options.AddDocumentTransformer<AddDefaultResponseTypeTransformer>();
    options.AddDocumentTransformer<RemoveInternalOperationsTransformer>();
    options.AddOperationTransformer<AddResponseDescriptionOperationTransformer>();
    options.AddSchemaTransformer<AddSchemaTransformer>();
});
builder.Services.AddOpenApi("v2", options =>
{
    options.AddDocumentTransformer<AddGeneralInformationTransformer>();
    options.AddDocumentTransformer<AddDefaultResponseTypeTransformer>();
    options.AddDocumentTransformer<RemoveInternalOperationsTransformer>();
    options.AddOperationTransformer<AddResponseDescriptionOperationTransformer>();
    options.AddSchemaTransformer<AddSchemaTransformer>();
});

builder.Services.AddScoped<ICityInfoBL, CityInfoBL>();
builder.Services.AddScoped<IProductBL, ProductBL>();
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
builder.Services.AddScoped<ICarvedRockRepository, CarvedRockRepository>();
builder.Services.AddTransient<IMailRepository, LocalMailRepository>();
//builder.Services.AddDbContext<CityInfoContext>(
//options => options.UseSqlite(builder.Configuration["ConnectionStrings:SqlLiteDBConnection"]));
builder.Services.AddDbContext<CityInfoContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:CityInfoDBConnection"]));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeFromBengaluru", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("city", "Bengaluru");
    });
});

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.ReportApiVersions = true;
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new ApiVersion(1, 0);
    setupAction.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddMvc()
.AddApiExplorer(op =>
{
    op.GroupNameFormat = "'v'VVV";
    op.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("openapi/{documentName}.json");
    app.MapScalarApiReference(option =>
    {
        option.WithTitle("Web API")
        .WithTheme(ScalarTheme.Purple)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/openapi/v1.json", "CityInfo API v1");
    //});
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
