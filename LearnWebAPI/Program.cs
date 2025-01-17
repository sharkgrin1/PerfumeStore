using System.ComponentModel.DataAnnotations;
using LearnWebAPI.Models;
using LearnWebAPI.Repositories;
using LearnWebAPI.services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PerfumeContext>(x => { x.UseInMemoryDatabase("Perfumes"); });

builder.Services.AddScoped<IPerfumeService, PerfumeService>();
builder.Services.AddScoped<IBrandService, BrandService>();

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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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
);

app.Run();