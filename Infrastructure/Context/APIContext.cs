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
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<User>()
            .HasKey(a => a.UserId);
        }
    }
}
