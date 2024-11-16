using AppointmentManagementSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? Id => Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value;
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        public bool IsAdmin => _httpContextAccessor.HttpContext?.User?.IsInRole("Admin") ?? false;
    }
}