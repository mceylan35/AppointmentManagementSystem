using AppointmentManagementSystem.Domain.Entities;
using AppointmentManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Service> Services { get; }
        DbSet<Appointment> Appointments { get; }
        DbSet<Role> Roles { get; }
        DbSet<Domain.Entities.UserRole> UserRoles { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    }
}
