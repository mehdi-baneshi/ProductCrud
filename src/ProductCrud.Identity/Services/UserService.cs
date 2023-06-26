using Microsoft.AspNetCore.Http;
using ProductCrud.Application.Constants;
using ProductCrud.Application.Contracts.Identity;

namespace ProductCrud.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> GetCurrentUserName()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uname)?.Value;

            return Task.FromResult(username);
        }
    }
}
