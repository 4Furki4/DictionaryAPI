using DictionaryAPI.Services;
using DictionaryAPI.Services.User_Service;

namespace DictionaryAPI.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILoggerService logger;
        private readonly IUserService userService;

        public AuthenticationMiddleware(RequestDelegate next, ILoggerService logger, IUserService userService)
        {
            this.next = next;
            this.logger = logger;
            this.userService = userService;
        }
        public async Task Invoke(HttpContext context)
        {
            logger.Write($"Get me: {userService.GetMe()}");

            await next(context);

            logger.Write($"Get me: {userService.GetMe()}");
        }
    }
}
