using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Infrastructure.Identity.Services.Abstract
{
    public interface IIdentityService
    { 
        Task<AuthenticationResult> AuthenticateAsync(string username, string password);
        Task<string> CreateUserAsync(User user, string password, string role = "User");
        Task<bool> IsInRoleAsync(Guid userId, string role);
        Task<List<string>> GetUserRolesAsync(Guid userId);
    }
}
