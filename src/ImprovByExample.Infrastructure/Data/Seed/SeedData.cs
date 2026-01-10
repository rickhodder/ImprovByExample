using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ImprovByExample.Domain.Entities;
using ImprovByExample.Domain.Enums;

namespace ImprovByExample.Infrastructure.Data.Seed;

public static class SeedData
{
    public static async Task SeedAsync(ImprovDbContext context, UserManager<ApplicationUser> userManager)
    {
        // Create admin user
        var adminUser = await EnsureAdminUser(userManager);

        // Seed Activity Types
        if (!await context.ActivityTypes.AnyAsync())
        {
            var activityTypes = new[]
            {
                new ActivityType { Name = "Game", Description = "Improv games and exercises", IsActive = true, CreatedById = adminUser.Id },
                new ActivityType { Name = "Technique", Description = "Improv techniques and principles", IsActive = true, CreatedById = adminUser.Id },
                new ActivityType { Name = "Warmup", Description = "Warmup exercises", IsActive = true, CreatedById = adminUser.Id },
                new ActivityType { Name = "Exercise", Description = "Training exercises", IsActive = true, CreatedById = adminUser.Id }
            };
            context.ActivityTypes.AddRange(activityTypes);
            await context.SaveChangesAsync();
        }

        // Seed Difficulties
        if (!await context.Difficulties.AnyAsync())
        {
            var difficulties = new[]
            {
                new Difficulty { Name = "Beginner", SortOrder = 1, IsActive = true, CreatedById = adminUser.Id },
                new Difficulty { Name = "Intermediate", SortOrder = 2, IsActive = true, CreatedById = adminUser.Id },
                new Difficulty { Name = "Advanced", SortOrder = 3, IsActive = true, CreatedById = adminUser.Id }
            };
            context.Difficulties.AddRange(difficulties);
            await context.SaveChangesAsync();
        }

        // Seed Relationship Types
        if (!await context.RelationshipTypes.AnyAsync())
        {
            var relationshipTypes = new[]
            {
                new RelationshipType { Name = "Alias", Description = "Same activity, different name", IsActive = true, CreatedById = adminUser.Id },
                new RelationshipType { Name = "Variation", Description = "Modified version of the activity", IsActive = true, CreatedById = adminUser.Id },
                new RelationshipType { Name = "Similar", Description = "Related but different activity", IsActive = true, CreatedById = adminUser.Id }
            };
            context.RelationshipTypes.AddRange(relationshipTypes);
            await context.SaveChangesAsync();
        }

        // Seed Video Generation Statuses
        if (!await context.VideoGenerationStatuses.AnyAsync())
        {
            var statuses = new[]
            {
                new VideoGenerationStatus { Name = "Queued", IsActive = true, CreatedById = adminUser.Id },
                new VideoGenerationStatus { Name = "Processing", IsActive = true, CreatedById = adminUser.Id },
                new VideoGenerationStatus { Name = "Complete", IsActive = true, CreatedById = adminUser.Id },
                new VideoGenerationStatus { Name = "Failed", IsActive = true, CreatedById = adminUser.Id }
            };
            context.VideoGenerationStatuses.AddRange(statuses);
            await context.SaveChangesAsync();
        }

        // Seed Social Media Post Statuses
        if (!await context.SocialMediaPostStatuses.AnyAsync())
        {
            var statuses = new[]
            {
                new SocialMediaPostStatus { Name = "Draft", IsActive = true, CreatedById = adminUser.Id },
                new SocialMediaPostStatus { Name = "Scheduled", IsActive = true, CreatedById = adminUser.Id },
                new SocialMediaPostStatus { Name = "Published", IsActive = true, CreatedById = adminUser.Id },
                new SocialMediaPostStatus { Name = "Failed", IsActive = true, CreatedById = adminUser.Id }
            };
            context.SocialMediaPostStatuses.AddRange(statuses);
            await context.SaveChangesAsync();
        }

        // Seed Activity Sources
        if (!await context.ActivitySources.AnyAsync())
        {
            var sources = new[]
            {
                new ActivitySource 
                { 
                    Name = "Truth in Comedy", 
                    Author = "Charna Halpern, Del Close, Kim Johnson",
                    SourceType = SourceType.Book,
                    Isbn = "9781566080033",
                    PublishedYear = 1994,
                    Description = "The manual and manifesto of The Harold",
                    CreatedById = adminUser.Id 
                },
                new ActivitySource 
                { 
                    Name = "Impro", 
                    Author = "Keith Johnstone",
                    SourceType = SourceType.Book,
                    Isbn = "9780878301171",
                    PublishedYear = 1979,
                    Description = "Improvisation and the Theatre",
                    CreatedById = adminUser.Id 
                },
                new ActivitySource 
                { 
                    Name = "improvencyclopedia.org", 
                    SourceType = SourceType.Website,
                    Url = "https://improvencyclopedia.org",
                    Description = "The improv encyclopedia",
                    CreatedById = adminUser.Id 
                },
                new ActivitySource 
                { 
                    Name = "learningimprov.com", 
                    SourceType = SourceType.Website,
                    Url = "https://learningimprov.com",
                    Description = "Improv learning resource",
                    CreatedById = adminUser.Id 
                }
            };
            context.ActivitySources.AddRange(sources);
            await context.SaveChangesAsync();
        }

        // Seed Initial Activities
        if (!await context.Activities.AnyAsync())
        {
            var gameType = await context.ActivityTypes.FirstAsync(t => t.Name == "Game");
            var warmupType = await context.ActivityTypes.FirstAsync(t => t.Name == "Warmup");
            var techniqueType = await context.ActivityTypes.FirstAsync(t => t.Name == "Technique");
            var exerciseType = await context.ActivityTypes.FirstAsync(t => t.Name == "Exercise");
            var beginnerDiff = await context.Difficulties.FirstAsync(d => d.Name == "Beginner");
            var intermediateDiff = await context.Difficulties.FirstAsync(d => d.Name == "Intermediate");
            var improvEncyclopedia = await context.ActivitySources.FirstAsync(s => s.Name == "improvencyclopedia.org");

            var activities = new[]
            {
                new ImprovActivity
                {
                    Name = "Zip Zap Zop",
                    ActivityTypeId = warmupType.Id,
                    ActivitySourceId = improvEncyclopedia.Id,
                    Description = "A classic energizer that helps improvisers focus and stay present",
                    Rules = "Players stand in a circle. One player points at another and says 'Zip'. That player points at someone else and says 'Zap'. The third player points at another and says 'Zop'. The pattern repeats: Zip, Zap, Zop. The goal is to maintain eye contact and keep energy high.",
                    Category = "Focus",
                    DifficultyId = beginnerDiff.Id,
                    MinPlayers = 3,
                    DurationMinutes = 5,
                    Tags = new[] { "energizer", "focus", "circle" },
                    CreatedById = adminUser.Id
                },
                new ImprovActivity
                {
                    Name = "Questions",
                    ActivityTypeId = gameType.Id,
                    ActivitySourceId = improvEncyclopedia.Id,
                    Description = "A scene where players can only speak in questions",
                    Rules = "Two players improvise a scene, but they can only speak using questions. If a player makes a statement, asks a rhetorical question, or hesitates too long, they lose and are replaced.",
                    Category = "Verbal",
                    DifficultyId = intermediateDiff.Id,
                    MinPlayers = 2,
                    MaxPlayers = 2,
                    DurationMinutes = 3,
                    Tags = new[] { "verbal", "scene", "challenge" },
                    CreatedById = adminUser.Id
                },
                new ImprovActivity
                {
                    Name = "Freeze Tag",
                    ActivityTypeId = gameType.Id,
                    ActivitySourceId = improvEncyclopedia.Id,
                    Description = "Players freeze and tag in to start new scenes based on physical positions",
                    Rules = "Two players start a scene. At any point, someone from the sideline yells 'Freeze!' The players freeze in their positions. The person who called freeze tags one player out and takes their exact physical position, then starts a completely new scene inspired by the position.",
                    Category = "Physicality",
                    DifficultyId = beginnerDiff.Id,
                    MinPlayers = 3,
                    DurationMinutes = 10,
                    Tags = new[] { "physical", "scene", "energy" },
                    CreatedById = adminUser.Id
                },
                new ImprovActivity
                {
                    Name = "185",
                    ActivityTypeId = gameType.Id,
                    ActivitySourceId = improvEncyclopedia.Id,
                    Description = "Rapid-fire joke telling format",
                    Rules = "Players stand in a line. The host gets a suggestion for a person or thing. Players take turns completing the phrase '185 [suggestions] walk into a bar...' with a punchline. Each player has just a few seconds to deliver their joke.",
                    Category = "Verbal",
                    DifficultyId = intermediateDiff.Id,
                    MinPlayers = 3,
                    DurationMinutes = 5,
                    Tags = new[] { "verbal", "puns", "quick thinking" },
                    CreatedById = adminUser.Id
                },
                new ImprovActivity
                {
                    Name = "Props",
                    ActivityTypeId = gameType.Id,
                    ActivitySourceId = improvEncyclopedia.Id,
                    Description = "Players use mundane objects in creative ways",
                    Rules = "Two teams of players take turns. A team picks up a prop (like a hose or piece of foam) and quickly demonstrates a creative use for it without speaking. The other team guesses. Teams alternate.",
                    Category = "Physicality",
                    DifficultyId = beginnerDiff.Id,
                    MinPlayers = 4,
                    DurationMinutes = 8,
                    Tags = new[] { "physical", "creative", "props" },
                    CreatedById = adminUser.Id
                },
                new ImprovActivity
                {
                    Name = "Yes And",
                    ActivityTypeId = techniqueType.Id,
                    Description = "The foundational principle of improv: accept and build",
                    Rules = "Accept whatever your scene partner offers (the 'Yes') and then add new information (the 'And'). This creates collaborative scenes where both players contribute. Never deny or block your partner's ideas.",
                    Category = "Fundamentals",
                    DifficultyId = beginnerDiff.Id,
                    MinPlayers = 2,
                    Tags = new[] { "fundamental", "collaboration", "acceptance" },
                    CreatedById = adminUser.Id
                },
                new ImprovActivity
                {
                    Name = "Mirror Exercise",
                    ActivityTypeId = exerciseType.Id,
                    Description = "Partner exercise for connection and awareness",
                    Rules = "Two players face each other. One leads, moving slowly and deliberately. The other mirrors their movements exactly. After a while, switch who leads. Eventually, try to move where neither is leading - both are following.",
                    Category = "Connection",
                    DifficultyId = beginnerDiff.Id,
                    MinPlayers = 2,
                    MaxPlayers = 2,
                    DurationMinutes = 5,
                    Tags = new[] { "physical", "connection", "awareness" },
                    CreatedById = adminUser.Id
                },
                new ImprovActivity
                {
                    Name = "Big Booty",
                    ActivityTypeId = warmupType.Id,
                    ActivitySourceId = improvEncyclopedia.Id,
                    Description = "Rhythmic number passing game",
                    Rules = "Players stand in a circle. One player is 'Big Booty', others are numbered sequentially. Everyone establishes a rhythm (slap legs, clap, snap, snap). Big Booty starts by saying 'Big Booty, Big Booty, Big Booty to number 5'. Number 5 responds 'Number 5, Number 5, Number 5 to number 2'. Continue the pattern. If someone messes up, they go to the end of the line and everyone renumbers.",
                    Category = "Focus",
                    DifficultyId = beginnerDiff.Id,
                    MinPlayers = 5,
                    DurationMinutes = 8,
                    Tags = new[] { "rhythm", "focus", "energy" },
                    CreatedById = adminUser.Id
                },
                new ImprovActivity
                {
                    Name = "Character Development",
                    ActivityTypeId = techniqueType.Id,
                    Description = "Creating rich, believable characters in the moment",
                    Rules = "Start with a physical choice: a walk, a gesture, a posture. Let the physicality inform the voice. Then add a want or need. What does this character desire? Finally, give them a point of view about the world. The character emerges from these specific choices.",
                    Category = "Character",
                    DifficultyId = intermediateDiff.Id,
                    MinPlayers = 1,
                    Tags = new[] { "character", "technique", "solo" },
                    CreatedById = adminUser.Id
                },
                new ImprovActivity
                {
                    Name = "Gibberish",
                    ActivityTypeId = exerciseType.Id,
                    ActivitySourceId = improvEncyclopedia.Id,
                    Description = "Scene work using made-up language",
                    Rules = "Players perform a scene speaking only gibberish (made-up sounds, not a real language). The goal is to communicate emotion, intent, and relationship through tone, physicality, and commitment rather than actual words.",
                    Category = "Communication",
                    DifficultyId = intermediateDiff.Id,
                    MinPlayers = 2,
                    DurationMinutes = 4,
                    Tags = new[] { "nonverbal", "emotion", "commitment" },
                    CreatedById = adminUser.Id
                }
            };

            context.Activities.AddRange(activities);
            await context.SaveChangesAsync();
        }
    }

    private static async Task<ApplicationUser> EnsureAdminUser(UserManager<ApplicationUser> userManager)
    {
        var adminEmail = "admin@improvbyexample.com";
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

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            // Add to Admin role
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        return adminUser;
    }
}
