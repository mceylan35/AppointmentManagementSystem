﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string ServiceName { get; set; }
        public Guid ServiceId { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
