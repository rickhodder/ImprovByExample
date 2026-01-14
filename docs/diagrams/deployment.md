# Deployment Diagrams

## CI/CD Pipeline

This diagram illustrates the continuous integration and deployment workflow from code commit to production.

```mermaid
graph TB
    subgraph Source["📝 Source Control"]
        Git[Git Push to GitHub<br/>main branch]
    end
    
    subgraph CI["🔨 Continuous Integration"]
        Trigger[GitHub Actions Triggered]
        Checkout[Checkout Code]
        Restore[Restore NuGet Packages<br/>dotnet restore]
        Build[Build Solution<br/>dotnet build]
        UnitTests[Run Unit Tests<br/>dotnet test UnitTests.csproj]
        IntegTests[Run Integration Tests<br/>dotnet test IntegrationTests.csproj]
        Quality{All Tests<br/>Pass?}
    end
    
    subgraph Package["📦 Containerization"]
        DockerBuild[Build Docker Images<br/>• API image<br/>• Web image]
        DockerTag[Tag Images<br/>• latest<br/>• version tag<br/>• git sha]
        DockerPush[Push to Container Registry<br/>Docker Hub / Azure ACR]
    end
    
    subgraph Deploy["🚀 Deployment"]
        Staging[Deploy to Staging<br/>• Azure App Service<br/>• AWS ECS<br/>• Self-hosted]
        SmokeTests[Run Smoke Tests<br/>Health checks<br/>Basic functionality]
        SmokePass{Smoke Tests<br/>Pass?}
        Production[Deploy to Production<br/>Blue-Green or Rolling]
        Rollback[Rollback to Previous<br/>Stable Version]
    end
    
    subgraph Monitor["📊 Monitoring"]
        Logs[Aggregate Logs<br/>Serilog → File/Cloud]
        Metrics[Application Metrics<br/>Performance<br/>Error rates]
        Alerts[Alert on Failures<br/>Email/Slack/PagerDuty]
    end
    
    Git --> Trigger
    Trigger --> Checkout
    Checkout --> Restore
    Restore --> Build
    Build --> UnitTests
    UnitTests --> IntegTests
    IntegTests --> Quality
    
    Quality -->|Pass| DockerBuild
    Quality -->|Fail| Alerts
    
    DockerBuild --> DockerTag
    DockerTag --> DockerPush
    DockerPush --> Staging
    
    Staging --> SmokeTests
    SmokeTests --> SmokePass
    
    SmokePass -->|Pass| Production
    SmokePass -->|Fail| Rollback
    Rollback --> Alerts
    
    Production --> Logs
    Production --> Metrics
    Metrics --> Alerts
    
    style Git fill:#f9f
    style Quality fill:#ff9
    style SmokePass fill:#ff9
    style Production fill:#9f9
    style Rollback fill:#f99
```

**Pipeline Stages:**

1. **Source Control**
   - Developer pushes code to GitHub
   - Triggers on `main` branch commits or pull requests

2. **Continuous Integration**
   - Restore dependencies
   - Build all projects
   - Run unit tests (fast, isolated)
   - Run integration tests (slower, database required)
   - Fail fast on any test failures

3. **Containerization**
   - Build Docker images for API and Web
   - Tag with version info (semver, git SHA)
   - Push to container registry

4. **Deployment**
   - Deploy to staging environment first
   - Run smoke tests (health endpoints, basic flows)
   - Deploy to production only if smoke tests pass
   - Support rollback on failures

5. **Monitoring**
   - Centralized logging with Serilog
   - Application performance monitoring
   - Automated alerts on errors/performance degradation

---

## Container Architecture

This diagram shows the Docker container structure and how services communicate.

```mermaid
graph TB
    subgraph External["🌍 External Services"]
        Users[Users<br/>Web Browsers]
        VideoAPI[Video Generation API<br/>External Service]
    end
    
    subgraph Docker["🐳 Docker Environment"]
        subgraph WebContainer["improv-web<br/>(Blazor Web App)"]
            WebApp[ASP.NET Core 8.0<br/>Port 5001<br/>Blazor Server]
        end
        
        subgraph APIContainer["improv-api<br/>(REST API)"]
            API[ASP.NET Core 8.0<br/>Port 5000<br/>REST Endpoints]
            SignalR[SignalR Hub<br/>WebSocket Support]
        end
        
        subgraph DBContainer["improv-db<br/>(PostgreSQL)"]
            Postgres[(PostgreSQL 16<br/>Port 5432<br/>Database)]
            PGData[(/var/lib/postgresql/data<br/>Persistent Volume)]
        end
    end
    
    subgraph Volumes["💾 Docker Volumes"]
        WebLogs[(web-logs<br/>Serilog Output)]
        APILogs[(api-logs<br/>Serilog Output)]
        DBVolume[(postgres-data<br/>Database Files)]
    end
    
    Users -->|HTTPS 443| WebApp
    Users -->|HTTPS 443| API
    
    WebApp -->|HTTP API Calls| API
    WebApp -->|SignalR Connection| SignalR
    
    API -->|Entity Framework| Postgres
    WebApp -.->|Future: Direct DB Read| Postgres
    
    API -->|HTTP Requests| VideoAPI
    
    Postgres --> PGData
    PGData --> DBVolume
    
    WebApp -.->|Write Logs| WebLogs
    API -.->|Write Logs| APILogs
    
    style Users fill:#e3f2fd
    style WebContainer fill:#f3e5f5
    style APIContainer fill:#fff3e0
    style DBContainer fill:#e8f5e9
    style VideoAPI fill:#fce4ec
```

**Container Details:**

### improv-web (Blazor Web App)
- **Base Image**: `mcr.microsoft.com/dotnet/aspnet:8.0`
- **Port**: 5001 (HTTPS)
- **Environment Variables**:
  - `ASPNETCORE_ENVIRONMENT`: Development/Staging/Production
  - `API_BASE_URL`: URL of improv-api container
  - `ConnectionStrings__ImprovDb`: PostgreSQL connection string
- **Volumes**:
  - `./src/ImprovByExample.Web/logs:/app/logs` - Log output
- **Dependencies**: improv-api, improv-db

### improv-api (REST API)
- **Base Image**: `mcr.microsoft.com/dotnet/aspnet:8.0`
- **Port**: 5000 (HTTP), 5001 (HTTPS)
- **Environment Variables**:
  - `ASPNETCORE_ENVIRONMENT`: Development/Staging/Production
  - `ConnectionStrings__ImprovDb`: PostgreSQL connection string
  - `VideoGeneration__ApiKey`: API key for video service
  - `VideoGeneration__Endpoint`: Video generation API endpoint
- **Volumes**:
  - `./src/ImprovByExample.Api/logs:/app/logs` - Log output
- **Dependencies**: improv-db

### improv-db (PostgreSQL)
- **Base Image**: `postgres:16`
- **Port**: 5432
- **Environment Variables**:
  - `POSTGRES_USER`: Database username
  - `POSTGRES_PASSWORD`: Database password
  - `POSTGRES_DB`: improv_db
- **Volumes**:
  - `postgres-data:/var/lib/postgresql/data` - Persistent database storage
- **Health Check**: `pg_isready -U postgres`

---

## Docker Compose Configuration

```mermaid
graph LR
    subgraph Compose["docker-compose.yml"]
        Services[Services<br/>• improv-web<br/>• improv-api<br/>• improv-db]
        Networks[Networks<br/>• improv-network<br/>  bridge mode]
        Volumes[Volumes<br/>• postgres-data<br/>• api-logs<br/>• web-logs]
    end
    
    subgraph Build["Build Context"]
        Dockerfile[Dockerfile<br/>Multi-stage build]
        Context[Build Context<br/>/src directory]
    end
    
    Services --> Networks
    Services --> Volumes
    Services --> Dockerfile
    Dockerfile --> Context
    
    style Compose fill:#e3f2fd
    style Build fill:#fff3e0
```

**docker-compose.yml Structure:**
```yaml
version: '3.8'

services:
  improv-db:
    image: postgres:16
    environment:
      POSTGRES_DB: improv_db
      POSTGRES_USER: improv_user
      POSTGRES_PASSWORD: ${DB_PASSWORD}
    volumes:
      - postgres-data:/var/lib/postgresql/data
    networks:
      - improv-network
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U improv_user"]
      interval: 10s
      timeout: 5s
      retries: 5

  improv-api:
    build:
      context: ./src
      dockerfile: ImprovByExample.Api/Dockerfile
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ConnectionStrings__ImprovDb=Host=improv-db;Database=improv_db;Username=improv_user;Password=${DB_PASSWORD}
      - VideoGeneration__ApiKey=${VIDEO_API_KEY}
    volumes:
      - ./src/ImprovByExample.Api/logs:/app/logs
    depends_on:
      improv-db:
        condition: service_healthy
    networks:
      - improv-network

  improv-web:
    build:
      context: ./src
      dockerfile: ImprovByExample.Web/Dockerfile
    ports:
      - "5002:80"
    environment:
      - API_BASE_URL=http://improv-api:80
    volumes:
      - ./src/ImprovByExample.Web/logs:/app/logs
    depends_on:
      - improv-api
    networks:
      - improv-network

networks:
  improv-network:
    driver: bridge

volumes:
  postgres-data:
  api-logs:
  web-logs:
```

---

## Deployment Environments

```mermaid
graph TB
    subgraph Development["💻 Development"]
        DevLocal[Local Machine<br/>Docker Desktop<br/>Visual Studio 2022]
        DevDB[(SQLite / PostgreSQL<br/>Local Instance)]
    end
    
    subgraph Staging["🧪 Staging"]
        StagingWeb[Azure App Service<br/>improv-staging-web]
        StagingAPI[Azure App Service<br/>improv-staging-api]
        StagingDB[(Azure PostgreSQL<br/>Flexible Server)]
    end
    
    subgraph Production["🚀 Production"]
        ProdWeb[Azure App Service<br/>improv-web<br/>Auto-scaling enabled]
        ProdAPI[Azure App Service<br/>improv-api<br/>Auto-scaling enabled]
        ProdDB[(Azure PostgreSQL<br/>High Availability<br/>Backup enabled)]
        CDN[Azure CDN<br/>Static assets<br/>wwwroot files]
    end
    
    DevLocal --> DevDB
    
    StagingWeb --> StagingAPI
    StagingAPI --> StagingDB
    
    ProdWeb --> ProdAPI
    ProdWeb --> CDN
    ProdAPI --> ProdDB
    
    DevLocal -.->|Deploy| StagingWeb
    StagingWeb -.->|Promote| ProdWeb
    
    style Development fill:#e3f2fd
    style Staging fill:#fff3e0
    style Production fill:#e8f5e9
```

**Environment Characteristics:**

| Environment | Purpose | Deployment | Database | Monitoring |
|-------------|---------|------------|----------|------------|
| **Development** | Local testing | Manual | Local PostgreSQL | Console logs |
| **Staging** | Pre-production validation | Automated (on merge to staging branch) | Azure PostgreSQL (smaller tier) | Application Insights |
| **Production** | Live users | Manual approval | Azure PostgreSQL (HA) | Full monitoring + alerts |

---

## Scaling Strategy

```mermaid
graph TB
    subgraph LB["⚖️ Load Balancer"]
        Azure[Azure Load Balancer<br/>or Application Gateway]
    end
    
    subgraph WebTier["🌐 Web Tier (Auto-scale)"]
        Web1[Web Instance 1]
        Web2[Web Instance 2]
        Web3[Web Instance N<br/>Scale based on CPU]
    end
    
    subgraph APITier["🔌 API Tier (Auto-scale)"]
        API1[API Instance 1]
        API2[API Instance 2]
        API3[API Instance N<br/>Scale based on requests]
    end
    
    subgraph DataTier["💾 Data Tier"]
        Primary[(Primary DB<br/>Read/Write)]
        Replica1[(Read Replica 1)]
        Replica2[(Read Replica 2)]
    end
    
    subgraph Cache["⚡ Cache Layer (Future)"]
        Redis[(Redis Cache<br/>Session state<br/>Query results)]
    end
    
    Azure --> Web1
    Azure --> Web2
    Azure --> Web3
    
    Web1 --> API1
    Web2 --> API2
    Web3 --> API3
    
    API1 --> Primary
    API2 --> Replica1
    API3 --> Replica2
    
    API1 -.->|Future| Redis
    API2 -.->|Future| Redis
    
    style LB fill:#e3f2fd
    style WebTier fill:#f3e5f5
    style APITier fill:#fff3e0
    style DataTier fill:#e8f5e9
    style Cache fill:#fce4ec
```

**Scaling Triggers:**
- **Web Tier**: Scale out when CPU > 70% for 5 minutes
- **API Tier**: Scale out when request queue > 100 or CPU > 70%
- **Database**: Add read replicas when read latency > 100ms
- **Cache**: Implement when query response time > 200ms consistently
