# Development Environment Setup

## Prerequisites

### Required Software
- **.NET 8 SDK** - [Download](https://dotnet.microsoft.com/download)
- **Docker Desktop** - [Download](https://www.docker.com/products/docker-desktop)
- **Visual Studio Code** or **Visual Studio 2022**
- **Git** - [Download](https://git-scm.com/downloads)

### Recommended Extensions (VS Code)
- C# Dev Kit
- Docker
- GitLens
- Thunder Client (for API testing)

## Initial Setup

### 1. Clone Repository
```bash
git clone https://github.com/rickhodder/ImprovByExample.git
cd ImprovByExample
```

### 2. Create Solution Structure
```bash
# Create solution
dotnet new sln -n ImprovByExample

# Create projects
dotnet new webapi -n ImprovByExample.Api -o src/ImprovByExample.Api
dotnet new web -n ImprovByExample.Web -o src/ImprovByExample.Web
dotnet new classlib -n ImprovByExample.Application -o src/ImprovByExample.Application
dotnet new classlib -n ImprovByExample.Domain -o src/ImprovByExample.Domain
dotnet new classlib -n ImprovByExample.Infrastructure -o src/ImprovByExample.Infrastructure

# Create test projects
dotnet new xunit -n ImprovByExample.UnitTests -o tests/ImprovByExample.UnitTests
dotnet new xunit -n ImprovByExample.IntegrationTests -o tests/ImprovByExample.IntegrationTests

# Add projects to solution
dotnet sln add src/ImprovByExample.Api
dotnet sln add src/ImprovByExample.Web
dotnet sln add src/ImprovByExample.Application
dotnet sln add src/ImprovByExample.Domain
dotnet sln add src/ImprovByExample.Infrastructure
dotnet sln add tests/ImprovByExample.UnitTests
dotnet sln add tests/ImprovByExample.IntegrationTests

# Add project references
dotnet add src/ImprovByExample.Api reference src/ImprovByExample.Application
dotnet add src/ImprovByExample.Api reference src/ImprovByExample.Infrastructure
dotnet add src/ImprovByExample.Web reference src/ImprovByExample.Application
dotnet add src/ImprovByExample.Web reference src/ImprovByExample.Infrastructure
dotnet add src/ImprovByExample.Application reference src/ImprovByExample.Domain
dotnet add src/ImprovByExample.Infrastructure reference src/ImprovByExample.Domain
dotnet add tests/ImprovByExample.UnitTests reference src/ImprovByExample.Domain
dotnet add tests/ImprovByExample.UnitTests reference src/ImprovByExample.Application
dotnet add tests/ImprovByExample.IntegrationTests reference src/ImprovByExample.Api
```

### 3. Local Development Environment
```bash
# Start local services (PostgreSQL, Redis)
docker-compose up -d

# Restore packages
dotnet restore

# Run database migrations
dotnet ef database update --project src/ImprovByExample.Infrastructure

# Start API
dotnet run --project src/ImprovByExample.Api

# Start Web UI (separate terminal)
dotnet run --project src/ImprovByExample.Web
```

## Development Workflow

### 1. Feature Development
1. Create feature branch: `git checkout -b feature/game-management`
2. Implement using TDD approach
3. Document AI interactions in `ai-learning/`
4. Run tests: `dotnet test`
5. Create PR with proper documentation

### 2. Testing
```bash
# Run unit tests
dotnet test tests/ImprovByExample.UnitTests

# Run integration tests
dotnet test tests/ImprovByExample.IntegrationTests

# Run all tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### 3. Docker Development
```bash
# Build application image
docker build -t improvbyexample-api .

# Run with docker-compose
docker-compose up --build

# View logs
docker-compose logs -f api
```

## Troubleshooting

### Common Issues

1. **PostgreSQL Connection Issues**
   - Ensure Docker Desktop is running
   - Check connection string in `appsettings.json`
   - Verify PostgreSQL container is healthy: `docker-compose ps`

2. **EF Core Migration Issues**
   - Ensure Infrastructure project is built
   - Check database connection
   - Run: `dotnet ef migrations add InitialCreate --project src/ImprovByExample.Infrastructure`

3. **Package Restore Issues**
   - Clear NuGet cache: `dotnet nuget locals all --clear`
   - Restore packages: `dotnet restore`

### Useful Commands
```bash
# Check solution build
dotnet build

# Watch for changes and rebuild
dotnet watch run --project src/ImprovByExample.Api

# Generate EF migration
dotnet ef migrations add MigrationName --project src/ImprovByExample.Infrastructure

# Update database
dotnet ef database update --project src/ImprovByExample.Infrastructure
```
