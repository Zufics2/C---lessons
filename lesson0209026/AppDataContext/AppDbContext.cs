using lesson0209026.Entities;
using Microsoft.EntityFrameworkCore;

namespace lesson0209026.AppDataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Order> Orders { get; set; }
        
    }
}