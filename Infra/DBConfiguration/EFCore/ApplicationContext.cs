using Config;
using Infra.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infra.DBConfiguration.EFCore
{
    public class ApplicationContext : DbContext
    {
        /* Creating DatabaseContext without Dependency Injection */
        public ApplicationContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                var service = RealityCoreConfiguration.GetService(RealityCoreConfiguration.Service);
                dbContextOptionsBuilder.UseSqlServer(service.ConnectionString);

                if (Debugger.IsAttached)
                {
                    dbContextOptionsBuilder
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging();
                }
            }
        }

        /* Creating DatabaseContext configured by Dependency Injection */
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InscricaoMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new FIleMap());
        }
    }
}
