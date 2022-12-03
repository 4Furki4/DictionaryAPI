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
            logger.Write(message: $"Origin: {context.Request.Headers.Origin}");
            logger.Write(message: $"AccessControlAllowMethods: {context.Request.Headers.AccessControlAllowMethods}");
            await next(context);
            logger.Write(message: $"Response Code: {context.Response.StatusCode}");
            logger.Write(message: $"AccessControlAllowOrigin: {context.Response.Headers.AccessControlAllowOrigin}");
            logger.Write(message: $"AccessControlAllowMethods: {context.Response.Headers.AccessControlAllowMethods}");
        }
    }
}
