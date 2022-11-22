using DictionaryAPI.Services;

namespace DictionaryAPI.Middlewares
{
    public class HttpInfoMiddleware 
    {
        private readonly ILoggerService logger;
        private RequestDelegate next;

        public HttpInfoMiddleware(RequestDelegate next, ILoggerService logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.Write("---- New request ----");
            logger.Write(message: $"Location: {context.Request.Path}");
            await next(context);
            logger.Write(message: $"Response Code: {context.Response.StatusCode}");
        }
    }
}
