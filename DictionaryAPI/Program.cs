using DictionaryAPI.Context;
using DictionaryAPI.Context.DictionaryRepository;
using DictionaryAPI.Context.DictionaryStore;
using DictionaryAPI.Middlewares;
using DictionaryAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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
var app = builder.Build();
app.UseHttpInfoMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
