using API_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Application.Infrastructure.Context
{
    public class APIContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public APIContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .HasKey(u => u.EmployeeId);

            modelBuilder.Entity<Employee>()
                .Property(u => u.EmpUuid)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Address>()
                .HasKey(a => a.AddressId);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Employee)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.EmpUuid)
                .HasPrincipalKey(u => u.EmpUuid);

            base.OnModelCreating(modelBuilder);
        }
    }
}
