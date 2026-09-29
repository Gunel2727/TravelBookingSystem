using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Interfaces;

namespace TBS.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid UserId
        {
            get
            {
                var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(userIdStr) ? Guid.Empty : Guid.Parse(userIdStr);
            }
        }
    }
}
