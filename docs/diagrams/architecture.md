# Architecture Diagrams

## Clean Architecture Layers

This diagram shows the four-layer clean architecture implementation with dependency flow from outer to inner layers.

```mermaid
graph TB
    subgraph Presentation["🎨 Presentation Layer"]
        Web[Blazor Web App<br/>Interactive UI Components]
        API[ASP.NET Core API<br/>REST Endpoints]
    end
    
    subgraph Application["⚙️ Application Layer"]
        Services[Business Services<br/>ActivityService]
        DTOs[DTOs & Models<br/>Request/Response Objects]
        Validators[FluentValidation<br/>Input Validation]
        Specs[Specifications<br/>Query Filters]
    end
    
    subgraph Infrastructure["🔧 Infrastructure Layer"]
        Repos[Repositories<br/>EF Core Implementation]
        EF[Entity Framework Core<br/>Data Access]
        Identity[ASP.NET Identity<br/>Authentication]
        SignalR[SignalR Hubs<br/>Real-time Updates]
    end
    
    subgraph Domain["💎 Domain Layer"]
        Entities[Domain Entities<br/>ImprovActivity, User, Show]
        Enums[Enums<br/>ActivityType, Difficulty]
    end
    
    Web --> Services
    API --> Services
    Services --> Repos
    Services --> Specs
    Repos --> EF
    EF --> Entities
    Identity --> Entities
    Services --> Validators
    Services --> DTOs
    
    style Presentation fill:#e1f5ff
    style Application fill:#fff4e1
    style Infrastructure fill:#ffe1f5
    style Domain fill:#e1ffe1
```

**Key Points:**
- Domain layer has no dependencies (pure business logic)
- Application layer depends only on Domain
- Infrastructure implements interfaces defined in Application
- Presentation depends on Application abstractions

---

## User Access Levels

This diagram illustrates the three access levels and their capabilities.

```mermaid
graph LR
    subgraph Anonymous["👤 Anonymous Users<br/>(No Login Required)"]
        Browse[📚 Browse Activities]
        Search[🔍 Search & Filter]
        View[👁️ View Details]
        Watch[🎥 Watch Videos]
        Plan[📋 Use Show Planner]
    end
    
    subgraph Authenticated["🔐 Authenticated Users<br/>(Standard Login)"]
        Login[✅ Login/Register]
        Save[💾 Save Shows]
        Favorites[⭐ Favorites<br/><i>Future Phase</i>]
        History[📜 View History<br/><i>Future Phase</i>]
    end
    
    subgraph Admin["👑 Admin Users<br/>(Full Access)"]
        Create[➕ Create Activities]
        Edit[✏️ Edit Content]
        Delete[🗑️ Delete Content]
        Generate[🎬 Generate Videos]
        Manage[📂 Manage Sources]
        Voice[🎤 Voice Input Mode]
    end
    
    Anonymous -.->|Register/Login| Authenticated
    Authenticated -.->|Admin Role| Admin
    
    style Anonymous fill:#e3f2fd
    style Authenticated fill:#f3e5f5
    style Admin fill:#fff3e0
```

**Access Level Progression:**
- Anonymous → Full read access, no account needed
- Authenticated → Personalization features (future phases)
- Admin → Full CRUD and content management

---

## Repository Pattern with Specifications

This class diagram shows the repository pattern implementation using the Specification pattern for flexible queries.

```mermaid
classDiagram
    class IReadRepository~T~ {
        <<interface>>
        +GetByIdAsync(int id) Task~T~
        +ListAsync(Specification spec) Task~List~T~~
        +CountAsync(Specification spec) Task~int~
        +FirstOrDefaultAsync(Specification spec) Task~T~
    }
    
    class IRepository~T~ {
        <<interface>>
        +AddAsync(T entity) Task~T~
        +UpdateAsync(T entity) Task
        +DeleteAsync(T entity) Task
        +SaveChangesAsync() Task
    }
    
    class EfRepository~T~ {
        -ImprovDbContext _context
        +GetByIdAsync(int id)
        +ListAsync(Specification spec)
        +AddAsync(T entity)
        +UpdateAsync(T entity)
        +DeleteAsync(T entity)
    }
    
    class Specification~T~ {
        <<abstract>>
        +Query IQueryable~T~
        #AddInclude(Expression include)
        #AddOrderBy(Expression orderBy)
        #AddWhere(Expression predicate)
    }
    
    class ActivitiesFilterSpec {
        +string SearchTerm
        +int? ActivityTypeId
        +int? DifficultyId
        +int? MinPlayers
        +int? MaxPlayers
        +ApplyFilters()
    }
    
    class ActivityByIdSpec {
        +int ActivityId
        +bool IncludeRelationships
        +bool IncludeVideoReferences
        +ApplyIncludes()
    }
    
    class ActivityService {
        -IRepository~ImprovActivity~ _repo
        +GetActivityAsync(int id)
        +ListActivitiesAsync(FilterDto filter)
        +CreateActivityAsync(CreateDto dto)
        +UpdateActivityAsync(UpdateDto dto)
    }
    
    IRepository~T~ --|> IReadRepository~T~ : extends
    EfRepository~T~ ..|> IRepository~T~ : implements
    Specification~T~ <|-- ActivitiesFilterSpec : inherits
    Specification~T~ <|-- ActivityByIdSpec : inherits
    ActivityService --> IRepository~T~ : uses
    ActivityService --> Specification~T~ : uses
    EfRepository~T~ --> Specification~T~ : applies
```

**Pattern Benefits:**
- **Separation of Concerns** - Query logic separated from data access
- **Testability** - Easy to mock repositories and test specifications
- **Reusability** - Specifications can be combined and reused
- **Flexibility** - Complex queries built through composition

---

## Component Dependencies

This diagram shows the project-level dependencies between solution components.

```mermaid
graph TD
    Web[ImprovByExample.Web<br/>Blazor Server]
    API[ImprovByExample.Api<br/>REST API]
    App[ImprovByExample.Application<br/>Business Logic]
    Infra[ImprovByExample.Infrastructure<br/>Data & Services]
    Domain[ImprovByExample.Domain<br/>Entities & Enums]
    UnitTests[ImprovByExample.UnitTests]
    IntegTests[ImprovByExample.IntegrationTests]
    
    Web --> App
    API --> App
    App --> Domain
    Infra --> App
    Infra --> Domain
    UnitTests --> App
    UnitTests --> Domain
    IntegTests --> API
    IntegTests --> Infra
    IntegTests --> Domain
    
    style Domain fill:#90EE90
    style App fill:#FFD700
    style Infra fill:#87CEEB
    style Web fill:#DDA0DD
    style API fill:#F08080
    style UnitTests fill:#F0E68C
    style IntegTests fill:#F0E68C
```

**Dependency Rules:**
- Domain has zero dependencies
- Application depends only on Domain
- Infrastructure implements Application interfaces
- Presentation layers depend on Application
- Tests reference what they test plus dependencies
