using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace DictionaryAPI.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpInfoMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpInfoMiddleware>();
        }

        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
