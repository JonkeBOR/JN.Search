using JN.Search.Application.Features.Search.MediatR;
using JN.Search.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<JN.Search.Infrastructure.Persistence.AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=search.db"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SearchServicesQuery).Assembly));
builder.Services.AddScoped<IProvidedServiceRepository, JN.Search.Infrastructure.Repositories.ProvidedServiceRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
