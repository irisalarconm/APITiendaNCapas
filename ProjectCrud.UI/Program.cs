using FluentValidation;
using ProjectCrud.BLL.Service;
using ProjectCrud.DAL.Data.Repository;
using ProjectCrud.Models;
using ProjectCrud.UI.Validators;
using Microsoft.Extensions.Logging;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

var MyCors = "MyCors";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGenericRepository<Client>, DataClient>();
builder.Services.AddScoped<IGenericRepository<Product>, DataProduct>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ClientValidator>();
builder.Services.AddScoped<ProductValidator>();
builder.Services.AddLogging(c =>
{
    c.AddFile($"log-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyCors, builder =>
    {
        builder.WithOrigins("*")
        .AllowAnyHeader().AllowAnyMethod();

    });
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
