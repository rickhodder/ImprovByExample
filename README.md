# ImprovByExample

A web application that manages a searchable database of improv comedy activities including games, techniques, warmups, and exercises. Built with .NET 10, Blazor, and PostgreSQL.

## Features

- 🎭 **Activity Database**: Comprehensive collection of improv games, techniques, warmups, and exercises
- 🔍 **Search & Filter**: Find activities by name, type, difficulty, or source
- 📚 **Source Attribution**: Proper attribution to books, websites, and workshops
- 🎥 **Video References**: External video links with timestamps (Phase 2)
- 🤝 **Activity Relationships**: Link related activities (aliases, variations, similar)
- 🔐 **Authentication**: Role-based access (Admin, StandardUser, Anonymous)
- 📊 **API Documentation**: Interactive API docs with Scalar
- 🧪 **Comprehensive Testing**: Unit, integration, and E2E tests

## Quick Start with Docker

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (Windows/Mac) or Docker Engine (Linux)
- 4GB RAM minimum (8GB recommended)

### Start the Application

```bash
# Clone the repository
git clone https://github.com/rickhodder/ImprovByExample.git
cd ImprovByExample

# Start all services (API, Web, PostgreSQL)
docker compose up -d

# Wait for services to start (first run takes 2-5 minutes)
docker compose logs -f
```

### Access the Application

- **Web UI**: http://localhost:5042
- **API**: http://localhost:5273
- **API Documentation**: http://localhost:5273/scalar/v1

### Default Admin Credentials

- **Email**: admin@improvbyexample.com
- **Password**: Admin123!

### Stop the Application

```bash
# Stop services (keeps data)
docker compose stop

# Stop and remove containers (keeps data)
docker compose down

# Complete cleanup (removes all data)
docker compose down -v
```

## Development Setup (Without Docker)

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL 16](https://www.postgresql.org/download/)

### Setup Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/rickhodder/ImprovByExample.git
   cd ImprovByExample
   ```

2. **Start PostgreSQL**
   ```bash
   # Using Docker
   docker run -d --name postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 postgres:16-alpine
   
   # Or install PostgreSQL locally
   ```

3. **Update connection string** (if needed)
   ```bash
   # Edit src/ImprovByExample.Api/appsettings.json
   # Update "ConnectionStrings:PostgreSQL" if using different credentials
   ```

4. **Run database migrations**
   ```bash
   dotnet ef database update --project src/ImprovByExample.Infrastructure --startup-project src/ImprovByExample.Api
   ```

5. **Start the API**
   ```bash
   dotnet run --project src/ImprovByExample.Api
   # API runs on http://localhost:5273
   ```

6. **Start the Web UI** (in a new terminal)
   ```bash
   dotnet run --project src/ImprovByExample.Web
   # Web runs on http://localhost:5042
   ```

## Testing

### Run All Tests
```bash
# Unit tests only
dotnet test tests/ImprovByExample.UnitTests

# Integration tests (requires Docker for PostgreSQL)
dotnet test tests/ImprovByExample.IntegrationTests

# All tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Test Summary
- **39 Unit Tests**: Validators, services, logging
- **13 Integration Tests**: API endpoints, authentication flow
- **Code Coverage**: Configured with Coverlet

## Project Structure

```
ImprovByExample/
├── src/
│   ├── ImprovByExample.Api/          # REST API
│   ├── ImprovByExample.Application/  # Business logic
│   ├── ImprovByExample.Domain/       # Domain models
│   ├── ImprovByExample.Infrastructure/ # Data access
│   └── ImprovByExample.Web/          # Blazor Web UI
├── tests/
│   ├── ImprovByExample.UnitTests/    # Unit tests
│   └── ImprovByExample.IntegrationTests/ # Integration tests
├── docs/
│   └── ImprovByExample-PRD.md        # Product requirements
├── docker-compose.yml                # Multi-service orchestration
├── PHASE10_USAGE.md                  # Docker usage guide
└── README.md                         # This file
```

## Architecture

### Technology Stack
- **Backend**: ASP.NET Core Web API (.NET 10)
- **Frontend**: Blazor Web App (Interactive Server mode)
- **Database**: PostgreSQL 16
- **ORM**: Entity Framework Core 10
- **UI Library**: MudBlazor (Material Design)
- **Logging**: Serilog with structured logging
- **Testing**: xUnit, FluentAssertions, Moq, Testcontainers
- **API Docs**: Scalar (OpenAPI)

### Design Patterns
- Clean Architecture (Domain → Application → Infrastructure → API/Web)
- Repository Pattern with Specifications (Ardalis.Specification)
- CQRS principles
- Dependency Injection
- FluentValidation for input validation

## CI/CD

The project includes a GitHub Actions workflow that runs on every push and pull request:

- ✅ Build and test (.NET solution)
- ✅ Run unit tests with coverage
- ✅ Build Docker images
- ✅ Run integration tests with PostgreSQL

See `.github/workflows/ci-cd.yml` for details.

## Documentation

- **[Product Requirements Document](docs/ImprovByExample-PRD.md)**: Complete product specification
- **[Docker Usage Guide](PHASE10_USAGE.md)**: Detailed Docker and deployment guide
- **[Phase Summaries](PHASE9_SUMMARY.md)**: Implementation history and progress

## Development Phases

### Completed ✅
- **Phase 1**: Blazor App Foundation with MudBlazor
- **Phase 2**: Clean Architecture & Domain Layer
- **Phase 3**: Database & EF Core Setup
- **Phase 4**: Repository Pattern & Specifications
- **Phase 5**: Identity & Authentication
- **Phase 6**: API Layer with Authorization
- **Phase 7**: Structured Logging with Serilog
- **Phase 8.5**: Authentication Implementation
- **Phase 9**: Data Seeding & Testing
- **Phase 10**: Deployment & CI/CD

### In Progress 🚧
- **Phase 2** (Features): External References & Activity Relationships

### Planned 📋
- **Phase 3**: AI Video Generation
- **Phase 4**: Show Planner
- **Phase 5**: Polish & Launch

## Contributing

This is a personal project, but feedback and suggestions are welcome! Please open an issue to discuss proposed changes.

## License

See [LICENSE](LICENSE) file for details.

## Support

For issues or questions:
1. Check the [Docker Usage Guide](PHASE10_USAGE.md) for troubleshooting
2. Review the [PRD](docs/ImprovByExample-PRD.md) for project details
3. Open an issue on GitHub

---

**Built with ❤️ for the improv community**