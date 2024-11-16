using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Application.DTOs.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Infrastructure.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IApplicationDbContext _context;

        public ServiceService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceDto>> GetAllActiveServicesAsync()
        {
            return await _context.Services
                .Where(s => s.IsActive)
                .Select(s => new ServiceDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description, 
                })
                .ToListAsync();
        }
    }
}
