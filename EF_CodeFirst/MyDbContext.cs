using Microsoft.EntityFrameworkCore;

namespace EF_CodeFirst;

public class MyDbContext : DbContext
{
    public DbSet<City> City { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ef_codefirst;Username=postgres;Password=7452");
        }
    }
}