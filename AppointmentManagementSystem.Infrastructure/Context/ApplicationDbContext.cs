using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppointmentManagementSystem.Domain.Entities;
using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Application.Common.Interfaces;

namespace AppointmentManagementSystem.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUser _currentUser; 

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUser currentUser
            ) : base(options)
        {
            _currentUser = currentUser; 
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUser.UserName;
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUser.UserName;
                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
