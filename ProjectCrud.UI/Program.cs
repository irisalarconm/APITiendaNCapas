using FluentValidation;
using ProjectCrud.BLL.Service;
using ProjectCrud.DAL.Data.Repository;
using ProjectCrud.Models;
using ProjectCrud.UI.Validators;
using Microsoft.Extensions.Logging;
using System.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var MyCors = "MyCors";
var FolderPath = "C:/platzi/SolutionCrud/ProjectCrud.UI/Loggers";
var logFilePath = Path.Combine(FolderPath, $"log-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt");
// Add services to the container.
//#pragma warning disable CS0618 // Type or member is obsolete
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.MSSqlServer(
//        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
//        tableName: "Logs",
//        autoCreateSqlTable: true)
//    .CreateLogger();
//#pragma warning restore CS0618 // Type or member is obsolete

builder.Services.AddControllers();
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
    c.AddFile(logFilePath);
    //c.AddSerilog();
});


//builder.Services.AddSingleton<Serilog.ILogger>();


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
