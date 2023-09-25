using Microsoft.EntityFrameworkCore;

namespace API_Application.Models
{
    public class APIContext : DbContext
    {        
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
        }
    }
}
