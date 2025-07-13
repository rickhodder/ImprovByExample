# PostgreSQL Learning Notes

## Current Comfort Level: 1/10

### What I Need to Learn

#### PostgreSQL Basics
- [ ] PostgreSQL vs SQL Server differences
- [ ] Basic PostgreSQL syntax and features
- [ ] Data types specific to PostgreSQL
- [ ] Performance characteristics

#### EF Core Integration
- [ ] Npgsql provider configuration
- [ ] PostgreSQL-specific EF Core features
- [ ] Migration handling with PostgreSQL
- [ ] Connection string configuration

#### Development Setup
- [ ] Running PostgreSQL in Docker
- [ ] Database management tools (pgAdmin)
- [ ] Connection and troubleshooting
- [ ] Backup and restore procedures

#### Advanced Features
- [ ] PostgreSQL-specific data types (JSON, arrays)
- [ ] Indexing strategies
- [ ] Performance optimization
- [ ] Security best practices

## Key Questions to Research
1. How does PostgreSQL differ from SQL Server in practice?
2. What EF Core configuration is needed for PostgreSQL?
3. How to handle PostgreSQL-specific features in EF Core?
4. Best practices for connection management?
5. Development workflow with containerized PostgreSQL?

## Learning Resources Needed
- PostgreSQL official documentation
- Npgsql and EF Core PostgreSQL guides
- Migration from SQL Server resources
- Performance and optimization guides

## Practical Goals
- [ ] Set up PostgreSQL in Docker
- [ ] Configure EF Core with PostgreSQL
- [ ] Create and run first migration
- [ ] Learn basic PostgreSQL management
- [ ] Understand connection string format

## Installation Options

### Option 1: Docker (Recommended)
```bash
# Pull PostgreSQL 15 image
docker pull postgres:15

# Run PostgreSQL container for development
docker run --name improvbyexample-postgres \
  -e POSTGRES_DB=improvbyexample \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=password \
  -p 5432:5432 \
  -v postgres_data:/var/lib/postgresql/data \
  -d postgres:15

# Or use the docker-compose.yml in your project
docker-compose up -d postgres
```

### Option 2: Windows Installer
- Download from [PostgreSQL Downloads](https://www.postgresql.org/download/windows/)
- Run installer and follow wizard
- Includes pgAdmin GUI tool
- Default port: 5432

### Option 3: macOS with Homebrew
```bash
brew install postgresql
brew services start postgresql
```

## Connection Details for Development

### Connection String for EF Core
```csharp
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=improvbyexample;Username=postgres;Password=password;Port=5432"
  }
}
```

### Docker Connection String
```csharp
// For containerized PostgreSQL
"Host=postgres;Database=improvbyexample;Username=postgres;Password=password;Port=5432"
```

## Essential Tools

### pgAdmin (GUI Management)
- **Docker**: `docker run -p 5050:80 -e PGADMIN_DEFAULT_EMAIL=admin@admin.com -e PGADMIN_DEFAULT_PASSWORD=admin -d dpage/pgadmin4`
- **Standalone**: Download from [pgAdmin](https://www.pgadmin.org/download/)

### Command Line Tools
```bash
# Connect to PostgreSQL
psql -h localhost -U postgres -d improvbyexample

# Basic commands
\l          # List databases
\dt         # List tables
\q          # Quit
```

## First Steps Setup

1. **Start PostgreSQL**
   ```bash
   docker-compose up -d postgres
   ```

2. **Install EF Core PostgreSQL Provider**
   ```bash
   dotnet add src/ImprovByExample.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL
   ```

3. **Configure DbContext**
   ```csharp
   services.AddDbContext<ApplicationDbContext>(options =>
       options.UseNpgsql(connectionString));
   ```

4. **Create First Migration**
   ```bash
   dotnet ef migrations add InitialCreate --project src/ImprovByExample.Infrastructure
   dotnet ef database update --project src/ImprovByExample.Infrastructure
   ```

## Common Issues and Solutions

### Connection Issues
- Check Docker container is running: `docker ps`
- Verify port 5432 is available: `netstat -an | findstr 5432`
- Check connection string format

### Migration Issues
- Ensure Npgsql package is installed
- Check connection string in appsettings.json
- Verify PostgreSQL is running before migrations

## Learning Progress Tracker

- [x] Choose installation method (Docker)
- [x] Set up PostgreSQL container
- [x] Install Docker extension for VS Code
- [x] Install PostgreSQL extension for VS Code
- [x] Create development scripts
- [x] Configure docker-compose for development
- [X] Install pgAdmin for GUI management - ckolkman.vscode-postgres is installed , so I dont need pgAdmin
- [ ] Configure EF Core with Npgsql
- [ ] Create first database and table
- [ ] Learn basic PostgreSQL commands
- [ ] Understand connection string format
- [ ] Successfully run first migration

## Docker Extension Setup Complete! ✅

### VS Code Extensions Installed
- [x] **Docker** (Microsoft) - `ms-azuretools.vscode-docker`
- [x] **PostgreSQL** (Chris Kolkman) - `ckolkman.vscode-postgres`

### Quick Start Commands
```bash
# Windows
.\scripts\setup-dev-environment.bat

# Linux/Mac
./scripts/setup-dev-environment.sh

# Or manually
docker-compose up -d postgres redis
```

### Using Docker Extension in VS Code

#### 1. **Docker Panel Access**
- Click Docker icon in sidebar OR
- `Ctrl+Shift+P` → "Docker: Focus on Containers View"

#### 2. **Container Management**
- **Start**: Right-click service → "Start"
- **Stop**: Right-click service → "Stop"  
- **Logs**: Right-click service → "View Logs"
- **Shell**: Right-click service → "Attach Shell"

#### 3. **Docker Compose Integration**
- Right-click `docker-compose.yml` → "Compose Up"
- Right-click `docker-compose.yml` → "Compose Down"

#### 4. **PostgreSQL Access**
- **GUI**: http://localhost:5050 (pgAdmin)
- **CLI**: Right-click postgres container → "Attach Shell" → `psql -U postgres`
- **VS Code**: Use PostgreSQL extension to connect

### Connection Details
```json
{
  "host": "localhost",
  "port": 5432,
  "database": "improvbyexample",
  "username": "postgres",
  "password": "password"
}
```

### Development Workflow
1. **Start Services**: `docker-compose up -d postgres redis`
2. **Check Status**: Use Docker panel in VS Code
3. **Connect to DB**: Use pgAdmin or PostgreSQL extension
4. **Run Migrations**: `dotnet ef database update`
5. **View Logs**: Right-click container → "View Logs"

### Troubleshooting with Docker Extension
- **Container won't start**: Check logs in Docker panel
- **Port conflicts**: Use Docker panel to see port mappings
- **Database issues**: Attach shell to postgres container
- **Connection issues**: Verify container health in Docker panel
