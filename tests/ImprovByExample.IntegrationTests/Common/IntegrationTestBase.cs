using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ImprovByExample.Infrastructure.Data;
using Testcontainers.PostgreSql;
using Respawn;

namespace ImprovByExample.IntegrationTests.Common;

public class IntegrationTestBase : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer;
    protected HttpClient Client = null!;
    protected WebApplicationFactory<Program> Factory = null!;
    private Respawner? _respawner;
    private string? _connectionString;

    protected IntegrationTestBase()
    {
        _postgresContainer = new PostgreSqlBuilder("postgres:16")
            .WithDatabase("improvbyexample_test")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();
        _connectionString = _postgresContainer.GetConnectionString();

        // Create the factory after the container has started
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing DbContext configuration
                    services.RemoveAll(typeof(DbContextOptions<ImprovDbContext>));
                    services.RemoveAll(typeof(ImprovDbContext));

                    // Add the test database configuration
                    services.AddDbContext<ImprovDbContext>(options =>
                    {
                        options.UseNpgsql(_connectionString);
                    });
                });

                builder.UseEnvironment("Testing");
            });

        Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        // Apply migrations and seed data
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ImprovDbContext>();
        await dbContext.Database.MigrateAsync();

        // Seed test data
        await Infrastructure.Data.Seed.DataSeeder.SeedDataAsync(scope.ServiceProvider);

        // Initialize Respawner for database cleanup between tests
        await using var connection = dbContext.Database.GetDbConnection();
        await connection.OpenAsync();
        
        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" },
            TablesToIgnore = new Respawn.Graph.Table[]
            {
                "__EFMigrationsHistory"
            }
        });
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
        Client.Dispose();
        await Factory.DisposeAsync();
    }

    protected async Task ResetDatabaseAsync()
    {
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ImprovDbContext>();
        await using var connection = dbContext.Database.GetDbConnection();
        await connection.OpenAsync();
        
        if (_respawner != null)
        {
            await _respawner.ResetAsync(connection);
        }
        
        // Re-seed data after reset
        await Infrastructure.Data.Seed.DataSeeder.SeedDataAsync(scope.ServiceProvider);
    }
}
