using Microsoft.EntityFrameworkCore;

namespace Scenius.CodeTest.Contracts;

public class DataContext : DbContext
{
    public DbSet<CalculationResults> CalculationResults { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=calculations;Username=postgres;Password=postgres");
}