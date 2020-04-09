using System.Linq;
using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor (IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }
        public string GetCurrentUsername ()
        {
            //when we created a token, we put a claim with the username in the nameidentifier
            var userName = _httpContextAccessor.HttpContext.User?.Claims?
                .FirstOrDefault (x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return userName;
        }
    }
}