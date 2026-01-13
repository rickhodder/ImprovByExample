# MVP Phase 10: Deployment & CI/CD - Usage Guide

## Overview
This guide explains how to use the containerization and CI/CD features implemented in MVP Phase 10. The application can now be run using Docker containers and includes automated testing and build pipelines.

## Prerequisites

### Required Software
- **Docker Desktop** (Windows/Mac) or **Docker Engine** (Linux)
  - Version 20.10 or higher
  - Docker Compose v2.0 or higher
- **.NET 10 SDK** (for local development without Docker)
- **Git** (for cloning and version control)

### System Requirements
- **Memory**: Minimum 4GB RAM (8GB recommended)
- **Disk Space**: 10GB free space for Docker images and volumes
- **OS**: Windows 10/11, macOS 11+, or Linux (Ubuntu 20.04+, Debian 11+, etc.)

## Docker Deployment

### Option 1: Using Docker Compose (Recommended)

Docker Compose runs all services (API, Web, PostgreSQL) with a single command.

#### Starting the Application

1. **Clone the repository** (if not already done):
   ```bash
   git clone https://github.com/rickhodder/ImprovByExample.git
   cd ImprovByExample
   ```

2. **Start all services**:
   ```bash
   docker compose up -d
   ```
   
   This command will:
   - Pull the PostgreSQL 16 Alpine image
   - Build the API Docker image
   - Build the Web Docker image
   - Create a Docker network for inter-service communication
   - Start all services in the background

3. **Wait for services to be ready** (first run takes 2-5 minutes):
   ```bash
   docker compose logs -f
   ```
   
   Press `Ctrl+C` to stop following logs.

4. **Access the application**:
   - **Web UI**: http://localhost:5042
   - **API**: http://localhost:5273
   - **API Documentation**: http://localhost:5273/scalar/v1 (Development mode)

#### Stopping the Application

```bash
# Stop all services (keeps data)
docker compose stop

# Stop and remove containers (keeps data in volumes)
docker compose down

# Stop, remove containers, and delete all data
docker compose down -v
```

#### Viewing Logs

```bash
# View all logs
docker compose logs

# Follow logs in real-time
docker compose logs -f

# View logs for specific service
docker compose logs api
docker compose logs web
docker compose logs postgres
```

#### Rebuilding After Code Changes

```bash
# Rebuild and restart services
docker compose up -d --build

# Rebuild specific service
docker compose up -d --build api
docker compose up -d --build web
```

### Option 2: Running Individual Docker Containers

If you prefer manual control, you can run containers separately.

#### 1. Start PostgreSQL

```bash
docker run -d \
  --name improvbyexample-postgres \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=improvbyexample \
  -p 5432:5432 \
  -v improvbyexample-postgres-data:/var/lib/postgresql/data \
  postgres:16-alpine
```

#### 2. Build and Run API

```bash
# Build API image
docker build -t improvbyexample/api:latest -f src/ImprovByExample.Api/Dockerfile .

# Run API container
docker run -d \
  --name improvbyexample-api \
  -p 5273:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e "ConnectionStrings__PostgreSQL=Host=host.docker.internal;Port=5432;Database=improvbyexample;Username=postgres;Password=postgres" \
  -v improvbyexample-api-logs:/app/logs \
  improvbyexample/api:latest
```

#### 3. Build and Run Web

```bash
# Build Web image
docker build -t improvbyexample/web:latest -f src/ImprovByExample.Web/Dockerfile .

# Run Web container
docker run -d \
  --name improvbyexample-web \
  -p 5042:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ApiSettings__BaseUrl=http://host.docker.internal:5273 \
  -v improvbyexample-web-logs:/app/logs \
  improvbyexample/web:latest
```

## CI/CD Pipeline

### GitHub Actions Workflow

The project includes an automated CI/CD pipeline that runs on every push and pull request.

#### Workflow Jobs

1. **Build and Test**
   - Restores NuGet packages
   - Builds the solution in Release configuration
   - Runs all unit tests
   - Generates code coverage reports
   - Uploads coverage to Codecov

2. **Docker Build**
   - Builds Docker images for API and Web
   - Uses GitHub Actions cache for faster builds
   - Only runs on push events (not PRs)

3. **Integration Tests**
   - Starts PostgreSQL in a Docker service
   - Runs all integration tests
   - Tests API endpoints with a real database

#### Viewing Workflow Results

1. Navigate to your repository on GitHub
2. Click the **Actions** tab
3. Select a workflow run to see detailed logs
4. Each job shows individual steps with their output

#### Manual Workflow Trigger

You can manually trigger the workflow from the Actions tab if needed.

## Environment Configuration

### Environment Variables

Both API and Web services support configuration via environment variables:

#### API Service

| Variable | Description | Default |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Environment name | `Production` |
| `ConnectionStrings__PostgreSQL` | Database connection string | (required) |
| `ASPNETCORE_URLS` | URLs to listen on | `http://+:8080` |

#### Web Service

| Variable | Description | Default |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Environment name | `Production` |
| `ApiSettings__BaseUrl` | API base URL | `http://localhost:5273` |
| `ASPNETCORE_URLS` | URLs to listen on | `http://+:8080` |

### Overriding Configuration

#### In docker-compose.yml

Edit the `environment` section for each service:

```yaml
services:
  api:
    environment:
      - ASPNETCORE_ENVIRONMENT=Staging
      - ConnectionStrings__PostgreSQL=Host=postgres;Port=5432;Database=improvbyexample;Username=postgres;Password=postgres
```

#### Using .env File

Create a `.env` file in the project root:

```env
ASPNETCORE_ENVIRONMENT=Development
POSTGRES_PASSWORD=your_secure_password
API_PORT=5273
WEB_PORT=5042
```

Then reference in docker-compose.yml:

```yaml
services:
  postgres:
    environment:
      POSTGRES_PASSWORD: ${POSTGRES_PASSWORD}
```

## Health Checks

### PostgreSQL Health Check

The docker-compose configuration includes a health check for PostgreSQL:

```bash
# Check if PostgreSQL is ready
docker exec improvbyexample-postgres pg_isready -U postgres
```

### Application Health Checks

You can verify services are running:

```bash
# Check API is responding
curl http://localhost:5273/api/activities

# Check Web is responding
curl http://localhost:5042
```

## Troubleshooting

### Common Issues

#### 1. Port Already in Use

**Error**: `bind: address already in use`

**Solution**: Stop the conflicting service or change the port mapping:

```yaml
ports:
  - "5274:8080"  # Changed from 5273
```

#### 2. Database Connection Failed

**Error**: `Connection refused` or `Cannot connect to database`

**Solution**: 
- Ensure PostgreSQL is running: `docker compose ps`
- Check logs: `docker compose logs postgres`
- Verify connection string in API environment variables

#### 3. Build Fails with Package Restore Errors

**Error**: `NU1301: Unable to load the service index`

**Solution**:
- Check internet connection
- Clear NuGet cache: `dotnet nuget locals all --clear`
- Retry the build

#### 4. Container Starts but Application Crashes

**Solution**:
- Check container logs: `docker compose logs api`
- Look for missing environment variables
- Verify database migrations have run

### Resetting Everything

To start fresh:

```bash
# Stop and remove all containers, networks, and volumes
docker compose down -v

# Remove Docker images
docker rmi improvbyexample/api improvbyexample/web

# Rebuild from scratch
docker compose up -d --build
```

## Production Deployment

### Deployment Options

The Dockerfiles are production-ready and can be deployed to:

1. **Azure Container Apps** (recommended)
   - Fully managed container platform
   - Auto-scaling based on load
   - Integrated with Azure services

2. **Azure Kubernetes Service (AKS)**
   - Full Kubernetes orchestration
   - Horizontal pod autoscaling
   - Best for high-traffic scenarios

3. **Docker Compose on VM**
   - Simple deployment to any Linux VM
   - Manual scaling
   - Cost-effective for small to medium traffic

4. **AWS ECS/Fargate**
   - AWS-managed container service
   - Serverless container option with Fargate

### Production Best Practices

1. **Use secrets management**:
   - Azure Key Vault
   - AWS Secrets Manager
   - Kubernetes Secrets

2. **Enable HTTPS**:
   - Use a reverse proxy (nginx, traefik)
   - Configure SSL certificates

3. **Set up monitoring**:
   - Application Insights
   - Prometheus + Grafana
   - ELK Stack for log aggregation

4. **Configure backups**:
   - Regular PostgreSQL backups
   - Volume snapshots

5. **Use production connection strings**:
   - Managed database service (Azure Database for PostgreSQL, AWS RDS)
   - Strong passwords
   - Connection pooling

## Development Workflow

### Recommended Development Flow

1. **Make code changes** in your IDE
2. **Test locally** without Docker:
   ```bash
   dotnet run --project src/ImprovByExample.Api
   dotnet run --project src/ImprovByExample.Web
   ```
3. **Rebuild Docker images** to test containerized version:
   ```bash
   docker compose up -d --build
   ```
4. **Commit and push** - CI pipeline runs automatically
5. **Monitor** GitHub Actions for test results

### Hot Reload in Docker (Optional)

For faster development, you can mount source code as volumes:

```yaml
services:
  api:
    volumes:
      - ./src/ImprovByExample.Api:/src/ImprovByExample.Api
```

However, this requires additional configuration for hot reload support.

## Additional Resources

- **Docker Documentation**: https://docs.docker.com/
- **Docker Compose Reference**: https://docs.docker.com/compose/compose-file/
- **GitHub Actions Documentation**: https://docs.github.com/en/actions
- **.NET in Docker**: https://learn.microsoft.com/en-us/dotnet/core/docker/introduction

## Support

For issues or questions:
1. Check the troubleshooting section above
2. Review Docker logs: `docker compose logs`
3. Open an issue on the GitHub repository
4. Consult the project PRD in `docs/ImprovByExample-PRD.md`
