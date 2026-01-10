using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Infrastructure.Data;

public class ImprovDbContextFactory : IDesignTimeDbContextFactory<ImprovDbContext>
{
    public ImprovDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ImprovDbContext>();
        
        // Use connection string for design-time migrations
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ImprovByExample;Username=postgres;Password=postgres;SSL Mode=Disable");
        
        return new ImprovDbContext(optionsBuilder.Options);
    }
}
