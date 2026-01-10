using ImprovByExample.Domain.Entities;
using ImprovByExample.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ImprovByExample.Infrastructure.Data.Seed;

public static class DataSeeder
{
    public static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ImprovDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Ensure database is created
        await context.Database.MigrateAsync();

        // Seed Roles
        await SeedRolesAsync(roleManager);

        // Seed Admin User
        var adminUser = await SeedAdminUserAsync(userManager);

        // Seed Lookup Data
        await SeedActivityTypesAsync(context, adminUser.Id);
        await SeedDifficultiesAsync(context, adminUser.Id);
        await SeedRelationshipTypesAsync(context, adminUser.Id);
        await SeedActivitySourcesAsync(context, adminUser.Id);
        
        // Seed Sample Activities
        await SeedSampleActivitiesAsync(context, adminUser.Id);

        await context.SaveChangesAsync();
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "StandardUser" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task<ApplicationUser> SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string adminEmail = "admin@improvbyexample.com";
        const string adminPassword = "Admin123!";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "User",
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        return adminUser;
    }

    private static async Task SeedActivityTypesAsync(ImprovDbContext context, string userId)
    {
        if (await context.ActivityTypes.AnyAsync())
            return;

        var activityTypes = new[]
        {
            new ActivityType
            {
                Name = "Game",
                Description = "Improvisation games and exercises for performance",
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ActivityType
            {
                Name = "Warmup",
                Description = "Exercises to warm up performers before a show or rehearsal",
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ActivityType
            {
                Name = "Technique",
                Description = "Fundamental improv techniques and principles",
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ActivityType
            {
                Name = "Exercise",
                Description = "Practice exercises for developing improv skills",
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        await context.ActivityTypes.AddRangeAsync(activityTypes);
    }

    private static async Task SeedDifficultiesAsync(ImprovDbContext context, string userId)
    {
        if (await context.Difficulties.AnyAsync())
            return;

        var difficulties = new[]
        {
            new Difficulty
            {
                Name = "Beginner",
                Description = "Suitable for newcomers to improv",
                SortOrder = 1,
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Difficulty
            {
                Name = "Intermediate",
                Description = "For performers with some improv experience",
                SortOrder = 2,
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Difficulty
            {
                Name = "Advanced",
                Description = "For experienced improvisers",
                SortOrder = 3,
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        await context.Difficulties.AddRangeAsync(difficulties);
    }

    private static async Task SeedRelationshipTypesAsync(ImprovDbContext context, string userId)
    {
        if (await context.RelationshipTypes.AnyAsync())
            return;

        var relationshipTypes = new[]
        {
            new RelationshipType
            {
                Name = "Alias",
                Description = "Same activity with a different name",
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new RelationshipType
            {
                Name = "Variation",
                Description = "A modified version of the activity",
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new RelationshipType
            {
                Name = "Similar",
                Description = "A related but different activity",
                IsActive = true,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        await context.RelationshipTypes.AddRangeAsync(relationshipTypes);
    }

    private static async Task SeedActivitySourcesAsync(ImprovDbContext context, string userId)
    {
        if (await context.ActivitySources.AnyAsync())
            return;

        var sources = new[]
        {
            new ActivitySource
            {
                SourceType = SourceType.Book,
                Name = "Impro: Improvisation and the Theatre",
                Author = "Keith Johnstone",
                Isbn = "978-0878301178",
                PublishedYear = 1979,
                Description = "A foundational text on improvisation and theatrical spontaneity",
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ActivitySource
            {
                SourceType = SourceType.Book,
                Name = "Truth in Comedy",
                Author = "Charna Halpern, Del Close, Kim Johnson",
                Isbn = "978-1566080033",
                PublishedYear = 1994,
                Description = "The manual for improvisation and long-form comedy",
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ActivitySource
            {
                SourceType = SourceType.Website,
                Name = "Improv Encyclopedia",
                Url = "https://improvencyclopedia.org",
                Description = "Comprehensive online database of improv games and exercises",
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ActivitySource
            {
                SourceType = SourceType.Original,
                Name = "ImprovByExample Original",
                Description = "Activities created specifically for this platform",
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        await context.ActivitySources.AddRangeAsync(sources);
    }

    private static async Task SeedSampleActivitiesAsync(ImprovDbContext context, string userId)
    {
        if (await context.Activities.AnyAsync())
            return;

        // Get the seeded data IDs
        var gameType = await context.ActivityTypes.FirstAsync(t => t.Name == "Game");
        var warmupType = await context.ActivityTypes.FirstAsync(t => t.Name == "Warmup");
        var techniqueType = await context.ActivityTypes.FirstAsync(t => t.Name == "Technique");
        var beginnerDiff = await context.Difficulties.FirstAsync(d => d.Name == "Beginner");
        var intermediateDiff = await context.Difficulties.FirstAsync(d => d.Name == "Intermediate");
        var improvEncSource = await context.ActivitySources.FirstAsync(s => s.Name == "Improv Encyclopedia");
        var johnstoneSource = await context.ActivitySources.FirstAsync(s => s.Name == "Impro: Improvisation and the Theatre");

        var activities = new[]
        {
            new ImprovActivity
            {
                Name = "Zip Zap Zop",
                ActivityTypeId = warmupType.Id,
                ActivitySourceId = improvEncSource.Id,
                Description = "A high-energy warm-up game that develops focus, energy, and group awareness",
                Rules = "Players stand in a circle. One player starts by making eye contact with another player, pointing at them, and saying 'Zip'. That player then points at another player and says 'Zap'. The third player continues the pattern by pointing and saying 'Zop'. The pattern then repeats: Zip, Zap, Zop. If a player makes a mistake, they're out (optional rule).",
                Category = "Energy",
                DifficultyId = beginnerDiff.Id,
                MinPlayers = 3,
                MaxPlayers = null,
                DurationMinutes = 5,
                Tags = new[] { "focus", "energy", "warmup", "circle" },
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ImprovActivity
            {
                Name = "Yes, And",
                ActivityTypeId = techniqueType.Id,
                ActivitySourceId = johnstoneSource.Id,
                Description = "The fundamental principle of improv: accept what your scene partner offers and build upon it",
                Rules = "In any scene or exercise, accept your partner's offers (the 'Yes') and then contribute something new to build the scene (the 'And'). Never deny, block, or negate what has been established.",
                Category = "Fundamentals",
                DifficultyId = beginnerDiff.Id,
                MinPlayers = 2,
                MaxPlayers = null,
                DurationMinutes = null,
                Tags = new[] { "fundamental", "technique", "acceptance", "building" },
                Script = "Player 1: \"What a beautiful day for a picnic!\"\nPlayer 2: \"Yes, and I brought your favorite sandwiches!\"\nPlayer 1: \"Yes, and look, the ducks are coming over to visit us!\"\nPlayer 2: \"Yes, and one of them is wearing a tiny hat!\"",
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ImprovActivity
            {
                Name = "Freeze Tag",
                ActivityTypeId = gameType.Id,
                ActivitySourceId = improvEncSource.Id,
                Description = "A dynamic game where players freeze in position and new players jump in to start a new scene based on the frozen position",
                Rules = "Two players start a scene. At any point, another player can yell 'Freeze!' The players on stage immediately freeze in their current positions. The player who called freeze taps out one of the frozen players and takes their exact position. They then start a completely new scene inspired by the frozen position, while maintaining that physical position.",
                Category = "Performance",
                DifficultyId = intermediateDiff.Id,
                MinPlayers = 3,
                MaxPlayers = null,
                DurationMinutes = 15,
                Tags = new[] { "physical", "scene work", "rotation", "energy" },
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        await context.Activities.AddRangeAsync(activities);
    }
}
