using Microsoft.AspNetCore.Http;
using ProjectTaskManagement.Application.Interfaces;
using System.Security.Claims;

namespace ProjectTaskManagement.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userId = _httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirst(ClaimTypes.NameIdentifier)?
                    .Value;

                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User Id not found in token");

                return int.Parse(userId);
            }
        }
    }
}