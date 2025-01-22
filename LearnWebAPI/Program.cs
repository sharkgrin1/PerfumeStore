using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Xml.Serialization;
using Asp.Versioning;
using LearnWebAPI.Config;
using LearnWebAPI.Models;
using LearnWebAPI.Repositories;
using LearnWebAPI.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

builder.Services.AddDbContext<PerfumeContext>(x => { x.UseInMemoryDatabase("Perfumes"); });

builder.Services.AddScoped<IPerfumeService, PerfumeService>();
builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("X-API-VERSION"),
        new MediaTypeApiVersionReader(),
        new QueryStringApiVersionReader("api-version")
    );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(365);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PerfumeContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .HasApiVersion(new ApiVersion(2, 0))
    .ReportApiVersions()
    .Build();

app.MapGet("/brand/{id:int}", (int id, IBrandService brandService) =>
{
    var brand = brandService.GetOne(id);
    return brand is not null ? Results.Ok(brand) : Results.NotFound();
});

app.MapGet("/brand",
        ([AsParameters] MinPagination pagination, IBrandService brandService) =>
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(pagination);
            return !Validator.TryValidateObject(pagination, validationContext, validationResults, true)
                ? Results.BadRequest(validationResults)
                : Results.Ok(brandService.GetAll(pagination));
        }
    )
    .WithApiVersionSet(apiVersionSet)
    .HasApiVersion(new ApiVersion(1, 0));

app.MapGet("/brand",
        ([AsParameters] MinPagination pagination, IBrandService brandService) =>
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(pagination);

            int statusCode;
            object result;
            if (!Validator.TryValidateObject(pagination, validationContext, validationResults, true))
            {
                result = validationResults;
                statusCode = 400;
            }
            else
            {
                result = brandService.GetAll(pagination);
                statusCode = 200;
            }

            var xmlSerializer = new XmlSerializer(result.GetType());
            using var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, result);

            return Results.Content(content: stringWriter.ToString(), contentType: MediaTypeNames.Application.Xml,
                statusCode: statusCode);
        }
    )
    .WithApiVersionSet(apiVersionSet)
    .HasApiVersion(new ApiVersion(2, 0));

app.Run();