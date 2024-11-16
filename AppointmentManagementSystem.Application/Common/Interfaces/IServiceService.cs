using AppointmentManagementSystem.Application.DTOs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Common.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetAllActiveServicesAsync();
    }
}
