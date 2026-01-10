using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ImprovByExample.Infrastructure.Data;

public class ImprovDbContextFactory : IDesignTimeDbContextFactory<ImprovDbContext>
{
    public ImprovDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ImprovDbContext>();
        
        // Default connection string for migrations
        optionsBuilder.UseNpgsql("Host=localhost;Database=improvbyexample;Username=postgres;Password=postgres");
        
        return new ImprovDbContext(optionsBuilder.Options);
    }
}
