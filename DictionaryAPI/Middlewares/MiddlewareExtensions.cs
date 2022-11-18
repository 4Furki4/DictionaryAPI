namespace DictionaryAPI.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpInfoMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpInfoMiddleware>();
        }
    }
}
