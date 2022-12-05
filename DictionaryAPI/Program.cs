using DictionaryAPI.Context;
using DictionaryAPI.Middlewares;
using DictionaryAPI.Services;
using DictionaryAPI.Services.User_Service;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var policies = builder.Configuration.GetSection("CORS:Origins").GetChildren().Select(x => x.Value).ToArray();


ValidatorOptions.Global.LanguageManager.Enabled = false;
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( opt =>
{
    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "bearer {token}",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    opt.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:TokenSecurityKey").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDbContext<DictionaryDB>
(
    opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DictionaryDB"]), 
    ServiceLifetime.Singleton
);
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();

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
 
app.UseRouting();
app.UseCors(opt => opt.WithOrigins("http://192.168.1.111:5500", "http://localhost:5000"));
app.UseCors(opt => opt.AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
