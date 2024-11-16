using AppointmentManagementSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentManagementSystem.Application.Common.Interfaces;

namespace AppointmentManagementSystem.Infrastructure.Context
{
    public class DesignTimeCurrentUserService : ICurrentUser
    {
        public Guid? Id => Guid.Parse("00000000-0000-0000-0000-000000000000");
        public string UserName => "System";
        public bool IsAuthenticated => true;
        public bool IsAdmin => true;
    }
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {


            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer("Server=DESKTOP-PM650FE\\SQLEXPRESS;Integrated Security=true;Database=AppointmentManagementDB;TrustServerCertificate=True;");
            
            return new(dbContextOptionsBuilder.Options, new DesignTimeCurrentUserService());


            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

   

          
 
        }
    }
    
}
