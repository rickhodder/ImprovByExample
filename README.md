# ImprovByExample

A comprehensive platform for improv comedy practitioners to discover, learn, and organize improv games, techniques, and performances.

## 🎭 Project Overview

ImprovByExample helps improv instructors and performers manage their repertoire of games, create balanced shows, and document performances. The application provides intelligent cataloging, automated scheduling, and professional documentation generation.

## 🚀 Features

### Current Features (MVP)
- **Game Management**: Create, edit, and organize improv games, techniques, and warmups
- **Categorization**: Tag games by type, style, and player requirements
- **Search & Filter**: Find games quickly using various criteria

### Planned Features
- **Show Builder**: Create shows with balanced player assignments
- **PDF Generation**: Generate professional show programs
- **Video Catalog**: Index improv examples with time-based tagging
- **MCP Integration**: Scheduling and document generation services

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core Web API, Razor Pages
- **Database**: PostgreSQL
- **Caching**: Redis
- **Architecture**: Clean Architecture with Fast Endpoints
- **Containerization**: Docker
- **Deployment**: AWS ECS
- **Testing**: xUnit, Integration Tests

## 📁 Project Structure

```
ImprovByExample/
├── src/
│   ├── ImprovByExample.Api/          # Web API endpoints
│   ├── ImprovByExample.Web/          # Razor Pages UI
│   ├── ImprovByExample.Application/  # Business logic
│   ├── ImprovByExample.Domain/       # Core domain models
│   └── ImprovByExample.Infrastructure/ # External concerns
├── tests/
│   ├── ImprovByExample.UnitTests/
│   └── ImprovByExample.IntegrationTests/
├── docs/                             # Documentation
├── ai-learning/                      # AI-assisted development notes
└── docker/                           # Container configurations
```

## 🏁 Getting Started

### Prerequisites

- .NET SDK
- Docker
- PostgreSQL
- Redis
- AWS Account

### Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/rickhodder/ImprovByExample.git
   cd ImprovByExample
   ```

2. **Create the Solution and Project Structure**:
   ```bash
   dotnet new sln -n ImprovByExample
   dotnet new webapi -n ImprovByExample.Api
   dotnet new classlib -n ImprovByExample.Application
   dotnet new classlib -n ImprovByExample.Domain
   dotnet new classlib -n ImprovByExample.Infrastructure
   dotnet new web -n ImprovByExample.Web
   ```

3. **Add Projects to Solution**:
   ```bash
   dotnet sln add src/ImprovByExample.Api/ImprovByExample.Api.csproj
   dotnet sln add src/ImprovByExample.Application/ImprovByExample.Application.csproj
   dotnet sln add src/ImprovByExample.Domain/ImprovByExample.Domain.csproj
   dotnet sln add src/ImprovByExample.Infrastructure/ImprovByExample.Infrastructure.csproj
   dotnet sln add src/ImprovByExample.Web/ImprovByExample.Web.csproj
   ```

4. **Run the Application**:
   ```bash
   cd src/ImprovByExample.Api
   dotnet run
   ```

## Contribution

Contributions are welcome! Please create a pull request for any enhancements or bug fixes.

## License

This project is licensed under the MIT License.

## 👤 Author

**Rick Hodder** - [GitHub](https://github.com/rickhodder)

---

*This project demonstrates modern .NET development practices with Clean Architecture, containerization, and AI-assisted development techniques.*