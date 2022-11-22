using DictionaryAPI.Services;

namespace DictionaryAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private RequestDelegate next;
        ILoggerService loggerService;
        public ExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            this.next = next;
            this.loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                
                loggerService.Write($"Error Source: {ex.Source}\n------Error Message------\n{ex.Message}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
