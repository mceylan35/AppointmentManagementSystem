using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.DTOs
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
