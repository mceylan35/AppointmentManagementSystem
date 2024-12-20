﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.DTOs.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; } 
        public bool IsActive { get; set; } 
        public DateTime CreatedAt { get; set; } 
    }
}
 
