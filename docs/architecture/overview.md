# ImprovByExample Architecture Overview

## System Architecture

The application follows Clean Architecture principles with feature-based organization:

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Presentation  │    │   Application   │    │   Domain        │
│   Layer         │────│   Layer         │────│   Layer         │
│                 │    │                 │    │                 │
│ • Web UI        │    │ • Use Cases     │    │ • Entities      │
│ • API           │    │ • Specifications│    │ • Value Objects │
│ • Fast Endpoints│    │ • Validators    │    │ • Domain Events │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                        │                        │
         └────────────────────────┼────────────────────────┘
                                  │
                    ┌─────────────────┐
                    │ Infrastructure  │
                    │ Layer           │
                    │                 │
                    │ • EF Core       │
                    │ • PostgreSQL    │
                    │ • Redis         │
                    │ • External APIs │
                    └─────────────────┘
```

## Technology Stack

### Presentation Layer
- **ASP.NET Core Web API**: REST API endpoints
- **Razor Pages**: Server-side rendered UI
- **Fast Endpoints**: Minimal API approach for performance

### Application Layer
- **.NET 8**: Runtime and framework
- **MediatR**: Command/Query handling
- **FluentValidation**: Input validation
- **Specifications Pattern**: Business logic encapsulation

### Domain Layer
- **Pure C# Classes**: No external dependencies
- **Entity Framework Entities**: Domain models
- **Value Objects**: Immutable data structures
- **Domain Events**: Business event handling

### Infrastructure Layer
- **Entity Framework Core**: ORM
- **PostgreSQL**: Primary database
- **Redis**: Caching layer
- **iTextSharp**: PDF generation
- **AWS Services**: Cloud infrastructure

## Deployment Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   AWS ECS       │    │   AWS RDS       │    │   AWS ElastiCache│
│   (Web App)     │────│   (PostgreSQL)  │    │   (Redis)        │
│                 │    │                 │    │                 │
│ • Docker        │    │ • Managed DB    │    │ • Managed Cache │
│ • Load Balancer │    │ • Backups       │    │ • High Availability│
│ • Auto Scaling  │    │ • Monitoring    │    │ • Monitoring    │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

## Key Design Principles

1. **Separation of Concerns**: Each layer has distinct responsibilities
2. **Dependency Inversion**: Higher layers don't depend on lower layers
3. **Feature-Based Organization**: Code organized by business features
4. **Testability**: Loose coupling enables comprehensive testing
5. **Scalability**: Containerized deployment supports horizontal scaling

## Project Structure

```
src/
├── ImprovByExample.Api/          # Web API endpoints
├── ImprovByExample.Web/          # Razor Pages UI
├── ImprovByExample.Application/  # Business logic
├── ImprovByExample.Domain/       # Core domain models
└── ImprovByExample.Infrastructure/ # External concerns
```
