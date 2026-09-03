using lesson0209026.Entities;
using lesson0209026.Helpers;
using Microsoft.EntityFrameworkCore;

namespace lesson0209026.AppDataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasConversion(
                    v => Crypt.Encrypt(v),
                    v => Crypt.Decrypt(v)
                );
        }
        
    }
}