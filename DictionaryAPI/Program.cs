using DictionaryAPI.Context;
using DictionaryAPI.Middlewares;
using DictionaryAPI.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ValidatorOptions.Global.LanguageManager.Enabled = false;
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DictionaryDB>
    (
        opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DictionaryDB"]), 
        ServiceLifetime.Singleton
    );
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseHttpInfoMiddleware();
app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
