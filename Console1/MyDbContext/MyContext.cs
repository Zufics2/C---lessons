using Microsoft.EntityFrameworkCore;
using Console1.Model;

namespace Console1.MyDbContext;

public class MyContext : DbContext
{
    // public MyContext(DbContextOptions<MyContext> options) : base(options)
    // {
    //     
    // }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cstest;Username=postgres;Password=7452");
        }
    }
    
    public DbSet<City> city { get; set; }
}