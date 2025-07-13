# Development Environment Setup

## Prerequisites

### Required Software
- **.NET 8 SDK** - [Download](https://dotnet.microsoft.com/download)
- **Docker Desktop** - [Download](https://www.docker.com/products/docker-desktop)
- **Visual Studio Code** or **Visual Studio 2022**
- **Git** - [Download](https://git-scm.com/downloads)

### Recommended Extensions (VS Code)
- C# Dev Kit
- Docker (Microsoft)
- PostgreSQL (Chris Kolkman)
- GitLens
- Thunder Client (for API testing)

## Initial Setup

### 1. Create GitHub Repository

#### Option A: Using GitHub Web Interface (Recommended)
1. Go to [GitHub.com](https://github.com) and sign in
2. Click "New repository" (green button)
3. Fill in repository details:
   - **Repository name**: `ImprovByExample`
   - **Description**: `A comprehensive platform for improv comedy practitioners to discover, learn, and organize improv games, techniques, and performances.`
   - **Visibility**: Public (recommended for portfolio)
   - **Initialize**: Leave unchecked (we'll add files locally)
4. Click "Create repository"

#### Option B: Using GitHub CLI (Advanced)
```bash
# Install GitHub CLI if not already installed
gh auth login
gh repo create ImprovByExample --public --description "A comprehensive platform for improv comedy practitioners to discover, learn, and organize improv games, techniques, and performances."
```

### 2. Set Up Local Repository

```bash
# Navigate to your project directory
cd D:\Github\ImprovByExample

# Initialize git repository
git init

# Add remote origin
git remote add origin https://github.com/rickhodder/ImprovByExample.git

# Verify remote is set correctly
git remote -v
```

### 3. Create Solution Structure
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

### 4. Initial Git Commit and Push

```bash
# Add all files to git
git add .

# Create initial commit
git commit -m "Initial project structure with Clean Architecture

- Created .NET 8 solution with Clean Architecture layers
- Added API, Web, Application, Domain, and Infrastructure projects
- Set up unit and integration test projects
- Added comprehensive documentation structure
- Configured Docker containerization
- Set up AI-assisted development tracking"

# Push to GitHub
git push -u origin main
```

### 5. Local Development Environment

```bash
# Start local services using the setup script
.\scripts\setup-dev-environment.bat

# OR manually start services
docker-compose -f docker/docker-compose.yml up -d postgres redis

# Restore packages
dotnet restore

# Run database migrations (after setting up EF Core)
dotnet ef database update --project src/ImprovByExample.Infrastructure

# Start API
dotnet run --project src/ImprovByExample.Api

# Start Web UI (separate terminal)
dotnet run --project src/ImprovByExample.Web
```

## Development Workflow

### Daily Development
1. **Pull latest changes**: `git pull origin main`
2. **Create feature branch**: `git checkout -b feature/game-management`
3. **Make changes and commit**: `git add . && git commit -m "Description"`
4. **Push and create PR**: `git push origin feature/game-management`

### Git Workflow
```bash
# Create and switch to feature branch
git checkout -b feature/your-feature-name

# Make your changes, then stage and commit
git add .
git commit -m "Descriptive commit message"

# Push feature branch
git push origin feature/your-feature-name

# Create pull request on GitHub
# After PR is merged, clean up
git checkout main
git pull origin main
git branch -d feature/your-feature-name
```

### Testing
```bash
# Run unit tests
dotnet test tests/ImprovByExample.UnitTests

# Run integration tests
dotnet test tests/ImprovByExample.IntegrationTests

# Run all tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Docker Development
```bash
# Build application image
docker build -f docker/Dockerfile -t improvbyexample-api .

# Run with docker-compose
docker-compose -f docker/docker-compose.yml up --build

# View logs
docker-compose -f docker/docker-compose.yml logs -f api
```

## Troubleshooting

### Common Issues

1. **Git Remote Issues**
   - Check remote URL: `git remote -v`
   - Reset remote: `git remote set-url origin https://github.com/rickhodder/ImprovByExample.git`

2. **Docker Compose File Location**
   - Ensure you're using: `docker-compose -f docker/docker-compose.yml`
   - Check file exists: `ls docker/docker-compose.yml`

3. **PostgreSQL Connection Issues**
   - Ensure Docker Desktop is running
   - Check connection string in `appsettings.json`
   - Verify PostgreSQL container is healthy: `docker-compose -f docker/docker-compose.yml ps`

4. **EF Core Migration Issues**
   - Ensure Infrastructure project is built
   - Check database connection
   - Run: `dotnet ef migrations add InitialCreate --project src/ImprovByExample.Infrastructure`

5. **Package Restore Issues**
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

# View Docker containers
docker-compose -f docker/docker-compose.yml ps

# View container logs
docker-compose -f docker/docker-compose.yml logs postgres
```

## VS Code Integration

### Docker Extension Usage
1. **Open Docker Panel**: `Ctrl+Shift+P` → "Docker: Focus on Containers View"
2. **Right-click containers** for options:
   - View logs
   - Attach shell
   - Start/stop containers
   - Inspect details

### PostgreSQL Extension Setup
1. **Connect to Database**: `Ctrl+Shift+P` → "PostgreSQL: New Connection"
   - Host: `localhost`
   - Port: `5432`
   - Database: `improvbyexample`
   - Username: `postgres`
   - Password: `password`

2. **Run Queries**: `Ctrl+Shift+P` → "PostgreSQL: New Query"
   - Write SQL and press `F5` to execute
   - View results inline

This setup gives you a clean, professional project structure with proper Git integration from the start!