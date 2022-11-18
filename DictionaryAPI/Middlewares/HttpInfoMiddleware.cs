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
            var requestPath = context.Request.Path;
            logger.Write(requestPath);
            await next(context);
            string message = $"Response Code: {context.Response.StatusCode}";
            logger.Write(message);
        }
    }
}
