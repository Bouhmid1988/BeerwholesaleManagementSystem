using BeerWholesaleManagementSystem.Core.DTO;
using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Core.Services;
using BeerWholesaleManagementSystem.Core.Validators;
using BeerWholesaleManagementSystem.Data;
using BeerWholesaleManagementSystem.Data.Repositories;
using BeerWholesaleManagementSystem.Services.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework Core with SQL Server for the database
builder.Services.AddDbContext<BeerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeerManagementDB")));

// Register repositories for dependency injection
builder.Services.AddTransient<IBeerRepository, BeerRepository>();
builder.Services.AddTransient<ISaleBeerRepositories, SaleBeerRepositories>();
builder.Services.AddTransient<IWholesalerRepository, WholesalerRepository>();
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<IQuoteRequestRepository, CommandRequestRepository>();
builder.Services.AddTransient<IQuoteRequestBeerDetailsRepository, CommandRequestBeerDetailsRepository>();


// Register services for dependency injection
builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddTransient<ISaleBeerService, SaleBeerService>();
builder.Services.AddTransient<IBeerService, BeerService>();
builder.Services.AddTransient<IWholesalerService, WholesalerService>();
builder.Services.AddScoped<IValidator<QuoteResquestDto>, QuoteRequestValidator>();


// Configure AutoMapper for object mapping
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
