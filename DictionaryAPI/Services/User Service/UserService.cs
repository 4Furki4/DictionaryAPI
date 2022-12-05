using System.Security.Claims;

namespace DictionaryAPI.Services.User_Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string GetMe()
        {
            var result = string.Empty;
            if (httpContextAccessor.HttpContext is not null)
            {
                result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            
            return result;
        }
    }
}
