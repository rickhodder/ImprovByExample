# Product Requirements Document: Improv By Example

## Table of Contents

### Core Product
- [1. Executive Summary](#1-executive-summary) - Product overview, users, and roles
- [2. Product Goals](#2-product-goals) - What success looks like
- [3. Core Features](#3-core-features) - What we're building (MVP + Phase 1-4)
  - [3.1 Activity Database](#31-improv-activity-database)
  - [3.2 AI Scripts](#32-ai-generated-scripts)
  - [3.3 AI Videos](#33-ai-generated-videos)
  - [3.4 Show Planner](#34-show-planner)

### Technical Details
- [4. Technical Architecture](#4-technical-architecture) - Stack, architecture, data models
  - [4.1 Technology Stack](#41-technology-stack)
  - [4.2 Architecture Overview](#42-architecture-overview)
  - [4.3 Data Model](#43-data-model)
- [5. Security & Privacy](#5-security--privacy) - Auth, authorization, cost control
- [6. User Experience](#6-user-experience) - User flows and performance requirements
- [7. Testing Strategy](#7-testing-strategy) - **Test-driven development approach**

### Implementation
- [8. Deployment & Containerization](#8-deployment--containerization) - Docker, CI/CD, deployment options
- [9. Development Phases](#9-development-phases) - **Start Here!** Phase-by-phase roadmap
  - [9.1 MVP Implementation Phases](#91-mvp-implementation-phases) - Detailed technical setup steps
    - [MVP Phase 1: Blazor App Foundation](#mvp-phase-1-blazor-app-foundation)
    - [MVP Phase 2-10: Infrastructure & Features](#mvp-phase-2-clean-architecture--domain-layer)
  - [9.2 Feature Phases](#92-feature-phases) - Major functionality milestones
    - [Phase 1: Foundation (MVP)](#phase-1-foundation-mvp) - ✅ COMPLETE
    - [Phase 2: External References](#phase-2-external-references--activity-relationships) - 🚧 IN PROGRESS
    - [Phase 3: AI Video Generation](#phase-3-ai-video-generation)
    - [Phase 4: Show Planner](#phase-4-show-planner)
    - [Phase 5: Polish & Launch](#phase-5-polish--launch)
    - [Phase 6: Commerce (Future)](#phase-6-commerce-features-future)
    - [Phase 7: Marketing Automation (Future)](#phase-7-marketing-automation-future)

### Future Planning
- [10. Future Enhancements](#10-future-enhancements) - Features for later phases
- [11. Open Questions](#11-open-questions) - Decisions to be made
- [12. Appendix](#12-appendix) - Why we chose this tech stack

---

## 1. Executive Summary

**Product Name:** Improv By Example

**Description:** A web application that manages a searchable database of improv comedy activities including games, techniques, warmups, and exercises. The app uses AI to generate example scripts and demonstration videos for each activity, and provides an intelligent show planner to create balanced performance agendas.

**Target Users:**
- Improv comedy performers and teachers
- Comedy theater directors and show hosts
- Improv students and learners
- Comedy enthusiasts exploring improv techniques

**User Access Levels:**
- **Anonymous (Unauthenticated):** Can browse, search, watch videos, and use show planner - no login required
- **Standard User (Authenticated):** Same as anonymous, plus future personalized features (favorites, history, saved shows)
- **Admin (Authenticated):** All standard capabilities plus CRUD operations on activities, video generation, and content management

---

## 2. Product Goals

### Primary Goals
- Provide a comprehensive, searchable repository of improv activities (games, techniques, warmups, exercises)
- Demonstrate how improv activities are performed through AI-generated video examples
- Simplify show planning with AI-assisted player assignments and activity ordering
- Create a learning resource that makes improv more accessible

### Success Metrics
- Number of activities in database (across all types)
- Video generation success rate
- User engagement (searches, video views)
- Show planner usage and satisfaction

---

## 3. Core Features

### 3.1 Improv Activity Database

**Description:** A searchable and filterable database of improv activities including games, techniques, warmups, and exercises.

**Requirements:**
- Each activity entry includes:
  - Name
  - Type (Game, Technique, Warmup, Exercise, Other)
  - Source (Book, Website, Workshop, etc. - for attribution)
  - Description
  - Rules/Instructions (detailed)
  - Examples (written scenarios)
  - Category/tags
  - Difficulty level
  - Number of players required (if applicable)
  - Duration estimate
  - One or more AI-generated scripts
  - One or more AI-generated demonstration videos
  - External video references (YouTube, Vimeo, etc.) with:
    - Video URL
    - Description of how video demonstrates the activity
    - List of timestamps marking where the activity appears
  - Related activities with relationship types:
    - Alias (same activity, different name)
    - Variation (modified version of the activity)
    - Similar (related but different activity)

**User Stories:**
- As a standard user, I can search for activities by name or keyword
- As a standard user, I can filter activities by type (game, technique, warmup, etc.)
- As a standard user, I can filter activities by category, difficulty, or player count
- As a standard user, I can filter activities by source
- As a standard user, I can view detailed information about each activity
- As a standard user, I can see the source attribution for each activity
- As a standard user, I can click links to purchase books or access source materials
- As a standard user, I can view external video references with timestamp links
- As a standard user, I can click timestamps to jump to specific moments in videos
- As a standard user, I can see related activities (aliases, variations, similar)
- As a standard user, I can click through to view related activities
- As an admin, I can add, edit, or delete activities from the database
- As an admin, I can manage activity types, categories, and tags
- As an admin, I can add and manage activity sources (books, websites, workshops, etc.)
- As an admin, I can add affiliate links for book sources
- As an admin, I can assign sources to activities for proper attribution
- As an admin, I can add external video references with URLs, descriptions, and timestamps
- As an admin, I can edit or remove video references from activities
- As an admin, I can link activities together with relationship types (alias, variation, similar)
- As an admin, I can remove activity relationships
- As an admin, I can use voice mode to dictate activity details hands-free
- As an admin, I can speak activity name, rules, and description instead of typing

### 3.2 Activity Scripts

**Description:** Text field for example scripts that demonstrate how each improv activity is performed.

**Requirements:**
- Scripts are manually entered by admins
- Scripts should include 2-3 player dialogue (for games) or step-by-step examples (for techniques)
- Format as clear, readable content appropriate to activity type
- Store scripts as part of activity record
- Support multiple script variations per activity (future enhancement)

**User Stories:**
- As a standard user, I can view example scripts for activities that have them
- As an admin, I can add or edit scripts when creating/updating activities
- As an admin, I can leave script field empty for activities without examples

**Future Enhancement:**
- AI-generated scripts using Semantic Kernel (Phase 6+)

### 3.3 AI-Generated Videos

**Description:** Create demonstration videos showing how each activity is performed using AI video generation.

**Requirements:**
- Generate videos from activity description, rules, and optional script
- Videos should be 30-60 seconds long
- Support 16:9 aspect ratio
- Display generation progress to users
- Store video URLs in database
- Handle long-running generation (5-10+ minutes)

**User Stories:**
- As a standard user, I can view completed videos for any activity
- As an admin, I can request video generation for any activity
- As an admin, I can see real-time progress updates while videos are being generated (via SignalR)
- As a standard user, I can view completed videos directly in the app
- As an admin, I receive real-time notification when video generation completes
- As an admin, I can navigate away and return to see updated progress without refreshing

**Technical Requirements:**
- Background processing using .NET BackgroundService
- Database tracking of video generation requests
- Status tracking: Queued → Processing → Complete/Failed
- Real-time progress updates using SignalR (no polling)
- Integration with video generation APIs (RunwayML, Stable Diffusion Video, etc.)
- Use activity description + rules + optional script as input to video generation
- Graceful handling of failures and retries
- SignalR hub groups for targeted notifications

### 3.4 Show Planner

**Description:** AI-assisted tool to create balanced improv show agendas.

**Requirements:**
- Input: List of players, list of available activities (typically games)
- Output: Ordered list of activities with player assignments
- Constraints:
  - Distribute players evenly across activities
  - No player waits more than 3 activities between performances
  - Respect activity requirements (player count, difficulty progression)
  - Consider activity types (warmups first, high-energy games strategically placed)
- Generate printable show cards for host including:
  - Activity name and type
  - Rules summary
  - Assigned players

**User Stories:**
- As a standard user, I can input available players and activities
- As a standard user, I can let AI optimize the show order
- As a standard user, I can manually adjust AI suggestions
- As a standard user, I can print show cards with activity details and player assignments
- As an admin, I can save and manage show templates

**AI Implementation:**
- Use Semantic Kernel for optimization logic
- Consider using traditional algorithms (scheduling/optimization) enhanced with AI
- Server-side processing

---

## 4. Technical Architecture

### 4.1 Technology Stack

**Backend:**
- ASP.NET Core Web API
- .NET 10 (with planned upgrade to .NET 12 LTS in Q4 2026)
- Entity Framework Core 10
- Ardalis.Specification (Repository pattern with specifications)
- Semantic Kernel (AI orchestration)
- BackgroundService (async processing)
- SignalR (real-time video generation updates)
- Serilog (Structured logging with enrichers)
- FluentValidation (Input validation and business rules)
- Scalar (API documentation with OpenAPI)

**Testing:**
- xUnit (unit and integration tests)
- Moq (mocking framework)
- FluentAssertions (assertion library)
- Testcontainers (containerized integration tests)
- Playwright or bUnit (Blazor component testing)
- Coverlet (code coverage)
- Respawn (database cleanup between tests)

**Frontend:**
- Blazor Web App
- Auto render mode (server + WebAssembly)
- SignalR client (real-time updates)
- MudBlazor (Material Design component library)

**Database:**
- PostgreSQL

**Infrastructure:**
- .NET Aspire (orchestration & observability)
- Docker & Docker Compose
- Container registry (Docker Hub, Azure Container Registry, or GitHub Container Registry)
- PostgreSQL container
- Redis container (if needed for distributed scenarios)
- Kubernetes-ready (optional for production scaling)
- Azure/local hosting

**AI Services:**
- OpenAI/Azure OpenAI (script generation)
- RunwayML/Replicate/Stable Diffusion (video generation)
- Azure Speech Services or OpenAI Whisper (speech-to-text for voice mode)

### 4.2 Solution Structure

**Clean Architecture with .NET Aspire:**

```
ImprovByExample/
├── src/
│   ├── ImprovByExample.AppHost/              # .NET Aspire orchestration
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── ImprovByExample.ServiceDefaults/      # Shared Aspire service defaults
│   │   ├── Extensions.cs
│   │   └── HostingExtensions.cs
│   │
│   ├── ImprovByExample.Domain/               # Core domain models (no dependencies)
│   │   ├── Entities/
│   │   │   ├── ApplicationUser.cs
│   │   │   ├── ImprovActivity.cs
│   │   │   ├── ActivityType.cs
│   │   │   ├── ActivitySource.cs
│   │   │   ├── SourceType.cs
│   │   │   ├── Difficulty.cs
│   │   │   ├── RelationshipType.cs
│   │   │   ├── ExternalVideoReference.cs
│   │   │   ├── VideoPlatform.cs
│   │   │   ├── VideoTimestamp.cs
│   │   │   ├── ActivityRelationship.cs
│   │   │   ├── VideoGenerationRequest.cs
│   │   │   ├── VideoGenerationStatus.cs
│   │   │   ├── Show.cs
│   │   │   ├── ShowActivity.cs
│   │   │   ├── SocialMediaPost.cs
│   │   │   ├── SocialMediaPostStatus.cs
│   │   │   └── SocialMediaPostTemplate.cs
│   │   ├── Enums/
│   │   │   └── SocialMediaPlatform.cs
│   │   └── Common/
│   │       └── BaseEntity.cs
│   │
│   ├── ImprovByExample.Application/          # Business logic & interfaces
│   │   ├── Common/
│   │   │   ├── Interfaces/
│   │   │   │   ├── Repositories/
│   │   │   │   │   ├── IRepository.cs
│   │   │   │   │   ├── IReadRepository.cs
│   │   │   │   │   └── IActivityRepository.cs
│   │   │   │   ├── Services/
│   │   │   │   │   ├── IActivityService.cs
│   │   │   │   │   ├── IVideoGenerationService.cs
│   │   │   │   │   ├── IShowPlannerService.cs
│   │   │   │   │   └── IAIService.cs
│   │   │   │   └── IUnitOfWork.cs
│   │   │   ├── Models/
│   │   │   │   ├── DTOs/
│   │   │   │   │   ├── ActivityDto.cs
│   │   │   │   │   ├── CreateActivityDto.cs
│   │   │   │   │   ├── UpdateActivityDto.cs
│   │   │   │   │   ├── ActivityFilterDto.cs
│   │   │   │   │   ├── VideoReferenceDto.cs
│   │   │   │   │   └── ShowPlanDto.cs
│   │   │   │   └── Responses/
│   │   │   │       ├── PagedResult.cs
│   │   │   │       └── ServiceResult.cs
│   │   │   └── Exceptions/
│   │   │       ├── NotFoundException.cs
│   │   │       └── ValidationException.cs
│   │   ├── Services/
│   │   │   ├── ActivityService.cs
│   │   │   ├── SearchService.cs
│   │   │   ├── VideoGenerationService.cs
│   │   │   └── ShowPlannerService.cs
│   │   ├── Validators/
│   │   │   ├── ActivityValidator.cs
│   │   │   ├── VideoReferenceValidator.cs
│   │   │   └── ShowPlanValidator.cs
│   │   └── Specifications/
│   │       ├── ActiveActivitiesSpec.cs
│   │       ├── ActivitiesFilterSpec.cs
│   │       └── ActivityWithRelatedSpec.cs
│   │
│   ├── ImprovByExample.Infrastructure/       # Data access & external services
│   │   ├── Data/
│   │   │   ├── ImprovDbContext.cs
│   │   │   ├── Configurations/
│   │   │   │   ├── ActivityConfiguration.cs
│   │   │   │   ├── ActivitySourceConfiguration.cs
│   │   │   │   ├── VideoReferenceConfiguration.cs
│   │   │   │   └── ShowConfiguration.cs
│   │   │   ├── Migrations/
│   │   │   └── Seed/
│   │   │       ├── SeedData.cs
│   │   │       └── InitialActivities.cs
│   │   ├── Repositories/
│   │   │   ├── Repository.cs
│   │   │   ├── ReadRepository.cs
│   │   │   └── ActivityRepository.cs
│   │   ├── Services/
│   │   │   ├── OpenAIService.cs
│   │   │   ├── VideoGenerationService.cs
│   │   │   └── BackgroundVideoProcessor.cs
│   │   ├── Identity/
│   │   │   └── IdentityConfiguration.cs
│   │   └── SignalR/
│   │       └── VideoProgressHub.cs
│   │
│   ├── ImprovByExample.Api/                  # REST API
│   │   ├── Controllers/
│   │   │   ├── ActivitiesController.cs
│   │   │   ├── ActivitySourcesController.cs
│   │   │   ├── ActivityTypesController.cs
│   │   │   ├── VideoReferencesController.cs
│   │   │   ├── ShowsController.cs
│   │   │   ├── AuthController.cs
│   │   │   └── AdminController.cs
│   │   ├── Middleware/
│   │   │   ├── RateLimitingMiddleware.cs
│   │   │   ├── ExceptionHandlingMiddleware.cs
│   │   │   └── CurrentUserMiddleware.cs
│   │   ├── Filters/
│   │   │   └── ValidateModelAttribute.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── appsettings.Development.json
│   │
│   └── ImprovByExample.Web/                  # Blazor Web UI
│       ├── Components/
│       │   ├── Layout/
│       │   │   ├── MainLayout.razor
│       │   │   ├── NavMenu.razor
│       │   │   └── AdminLayout.razor
│       │   ├── Pages/
│       │   │   ├── Home.razor
│       │   │   ├── Browse.razor
│       │   │   ├── ActivityDetail.razor
│       │   │   ├── ShowPlanner.razor
│       │   │   ├── Admin/
│       │   │   │   ├── ManageActivities.razor
│       │   │   │   ├── ManageSources.razor
│       │   │   │   ├── VideoGeneration.razor
│       │   │   │   └── ManageUsers.razor
│       │   │   └── Auth/
│       │   │       ├── Login.razor
│       │   │       └── Register.razor
│       │   └── Shared/
│       │       ├── ActivityCard.razor
│       │       ├── SearchBar.razor
│       │       ├── VideoPlayer.razor
│       │       └── LoadingSpinner.razor
│       ├── Services/
│       │   ├── ApiClient.cs
│       │   └── SignalRService.cs
│       ├── Program.cs
│       ├── appsettings.json
│       └── wwwroot/
│
├── tests/
│   ├── ImprovByExample.UnitTests/
│   │   ├── Domain/
│   │   │   └── EntityTests.cs
│   │   ├── Application/
│   │   │   ├── Services/
│   │   │   │   ├── ActivityServiceTests.cs
│   │   │   │   └── ShowPlannerServiceTests.cs
│   │   │   ├── Validators/
│   │   │   │   └── ActivityValidatorTests.cs
│   │   │   └── Specifications/
│   │   │       └── ActivitySpecificationTests.cs
│   │   └── Infrastructure/
│   │       └── RepositoryTests.cs
│   │
│   ├── ImprovByExample.IntegrationTests/
│   │   ├── Api/
│   │   │   ├── ActivitiesApiTests.cs
│   │   │   ├── AuthApiTests.cs
│   │   │   └── ShowPlannerApiTests.cs
│   │   ├── Infrastructure/
│   │   │   ├── DatabaseTests.cs
│   │   │   └── SignalRTests.cs
│   │   └── Common/
│   │       ├── WebApplicationFactory.cs
│   │       └── TestContainersFixture.cs
│   │
│   └── ImprovByExample.E2ETests/
│       ├── Blazor/
│       │   ├── BrowsingTests.cs
│       │   ├── ActivityDetailTests.cs
│       │   └── ShowPlannerTests.cs
│       └── Playwright/
│           └── UserJourneyTests.cs
│
├── docs/
│   ├── ImprovByExample-PRD.md
│   ├── API.md
│   └── SETUP.md
│
├── .github/
│   └── workflows/
│       ├── ci.yml
│       └── cd.yml
│
├── .gitignore
├── README.md
├── ImprovByExample.sln
└── Directory.Build.props
```

**Key Design Principles:**

1. **Clean Architecture**: Dependencies flow inward (Domain ← Application ← Infrastructure ← API/Web)
2. **Domain Layer**: Pure C# with no external dependencies or frameworks
3. **Application Layer**: Business logic, interfaces, DTOs, specifications, validators
4. **Infrastructure Layer**: EF Core, repositories, external service integrations
5. **Presentation Layer**: API (REST) and Web (Blazor) as separate concerns
6. **Aspire Orchestration**: AppHost coordinates all services, databases, and observability
7. **Test Isolation**: Unit, Integration, and E2E tests in separate projects

### 4.3 Containerized Architecture

**Aspire Deployment Model:**

```
┌─────────────────────────────────────┐
│   .NET Aspire Host                  │
│   (Container Orchestration)         │
│                                     │
│  ┌─────────────────────────────┐   │
│  │  API Container              │   │
│  │  - ASP.NET Core Web API     │   │
│  │  - Semantic Kernel          │   │
│  │  - BackgroundService        │   │
│  │  - SignalR Hub              │   │
│  └──────────┬──────────────────┘   │
│             │                       │
│  ┌──────────▼──────────────────┐   │
│  │  PostgreSQL Container       │   │
│  │  - Persistent volume        │   │
│  └─────────────────────────────┘   │
│                                     │
│  ┌─────────────────────────────┐   │
│  │  Blazor Web Container       │   │
│  │  - Interactive UI           │   │
│  │  - SignalR client           │   │
│  └─────────────────────────────┘   │
└─────────────────────────────────────┘

All components run as Docker containers
Easily deployable to any container host
```

### 4.4 Data Model

**ApplicationUser** (extends IdentityUser)
- Id (string, PK) - Inherited from IdentityUser
- UserName (string) - Inherited
- Email (string) - Inherited
- EmailConfirmed (bool) - Inherited
- PasswordHash (string) - Inherited
- PhoneNumber (string?) - Inherited
- TwoFactorEnabled (bool) - Inherited
- LockoutEnd (DateTimeOffset?) - Inherited
- AccessFailedCount (int) - Inherited
- FirstName (string?)
- LastName (string?)
- CreatedAt (DateTime)
- LastLoginAt (DateTime?)

**Note:** ASP.NET Core Identity creates these tables automatically:
- AspNetUsers (stores ApplicationUser records)
- AspNetRoles (stores roles: StandardUser, Admin)
- AspNetUserRoles (many-to-many user-role assignments)
- AspNetUserClaims, AspNetUserLogins, AspNetUserTokens, AspNetRoleClaims

**ActivityType**
- Id (int, PK)
- Name (string) - Game, Technique, Warmup, Exercise, Other
- Description (string)
- IsActive (bool)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**SourceType** (Reference Entity)
- Id (int, PK)
- Name (string) - Book, Website, Workshop, Class, Person, Original
- Description (string)
- IsActive (bool)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**ActivitySource**
- Id (int, PK)
- SourceTypeId (int, FK to SourceType)
- SourceType (SourceType, navigation property)
- Name (string) - e.g., "Impro: Improvisation and the Theatre", "improvencyclopedia.org"
- Author (string, optional) - e.g., "Keith Johnstone"
- Url (string, optional) - Link to source if available
- AffiliateUrl (string, optional) - Amazon/bookstore affiliate link
- Isbn (string, optional) - For book sources
- PublishedYear (int, optional)
- Description (string, optional)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**Difficulty**
- Id (int, PK)
- Name (string) - Beginner, Intermediate, Advanced
- SortOrder (int) - For ordering by difficulty level
- Description (string, optional)
- IsActive (bool)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**RelationshipType**
- Id (int, PK)
- Name (string) - Alias, Variation, Similar
- Description (string, optional)
- IsActive (bool)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**VideoGenerationStatus**
- Id (int, PK)
- Name (string) - Queued, Processing, Complete, Failed
- Description (string, optional)
- IsActive (bool)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**SocialMediaPostStatus**
- Id (int, PK)
- Name (string) - Draft, Scheduled, Published, Failed
- Description (string, optional)
- IsActive (bool)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**ImprovActivity**
- Id (int, PK)
- Name (string)
- ActivityTypeId (int, FK)
- ActivitySourceId (int?, FK, optional)
- Description (string)
- Rules (string)
- Script (string?, optional) - Example script/dialogue demonstrating the activity
- Category (string)
- DifficultyId (int?, FK to Difficulty, optional)
- MinPlayers (int?)
- MaxPlayers (int?)
- DurationMinutes (int?)
- Tags (string[])
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**VideoPlatform** (Reference Entity)
- Id (int, PK)
- Name (string) - YouTube, Vimeo, Other
- Description (string)
- IsActive (bool)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**ExternalVideoReference**
- Id (int, PK)
- ActivityId (int, FK)
- Url (string)
- Description (string)
- VideoPlatformId (int, FK to VideoPlatform)
- VideoPlatform (VideoPlatform, navigation property)
- AddedById (string, FK to AspNetUsers)
- AddedBy (ApplicationUser, navigation property)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**VideoTimestamp**
- Id (int, PK)
- ExternalVideoReferenceId (int, FK)
- TimestampSeconds (int)
- Label (string)
- Description (string)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**ActivityRelationship**
- Id (int, PK)
- ActivityId (int, FK)
- RelatedActivityId (int, FK)
- RelationshipTypeId (int, FK to RelationshipType)
- Notes (string, optional)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**VideoGenerationRequest**
- Id (int, PK)
- ActivityId (int, FK)
- RequestedById (string, FK to AspNetUsers)
- RequestedBy (ApplicationUser, navigation property)
- StatusId (int, FK to VideoGenerationStatus)
- VideoUrl (string?)
- ErrorMessage (string?)
- Progress (int)
- CreatedAt (DateTime)
- CompletedAt (DateTime?)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**Show**
- Id (int, PK)
- Name (string)
- Date (DateTime)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**ShowActivity**
- Id (int, PK)
- ShowId (int, FK)
- ActivityId (int, FK)
- OrderIndex (int)
- Players (string[])
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**SocialMediaPost** (Future - Phase 7)
- Id (int, PK)
- ActivityId (int?, FK, optional)
- VideoGenerationRequestId (int?, FK, optional)
- Platform (enum: YouTube, TikTok, Instagram, Twitter, Facebook, LinkedIn)
- ContentHash (string) - For duplicate detection
- Caption (string)
- Hashtags (string[])
- StatusId (int, FK to SocialMediaPostStatus)
- ScheduledFor (DateTime?)
- PublishedAt (DateTime?)
- ExternalPostId (string?) - ID from social media platform
- ViewCount (int?)
- LikeCount (int?)
- ShareCount (int?)
- CommentCount (int?)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

**SocialMediaPostTemplate** (Future - Phase 7)
- Id (int, PK)
- Name (string)
- Platform (enum: YouTube, TikTok, Instagram, Twitter, Facebook, LinkedIn)
- CaptionTemplate (string) - With placeholders like {ActivityName}, {ActivityType}
- DefaultHashtags (string[])
- IsActive (bool)
- CreatedById (string, FK to AspNetUsers)
- CreatedBy (ApplicationUser, navigation property)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
- UpdatedById (string?, FK to AspNetUsers, optional)
- UpdatedBy (ApplicationUser?, navigation property)

### Repository Pattern with Specifications

**Specification Pattern Library:**
- Using **Ardalis.Specification** (NuGet package)
- Pre-built, production-ready implementation
- Optimized for Entity Framework Core
- Clean, testable query logic

**Repository Interfaces:**
```csharp
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class { }
public interface IRepository<T> : IRepositoryBase<T> where T : class { }

public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    public Repository(ImprovDbContext dbContext) : base(dbContext) { }
}
```

**Example Specifications:**

```csharp
// Active activities with includes
public class ActiveActivitiesSpec : Specification<ImprovActivity>
{
    public ActiveActivitiesSpec()
    {
        Query.Where(a => a.IsActive)
             .Include(a => a.ActivityType)
             .Include(a => a.ActivitySource)
             .Include(a => a.CreatedBy);
    }
}

// Search with filters
public class ActivitiesFilterSpec : Specification<ImprovActivity>
{
    public ActivitiesFilterSpec(ActivitiesFilterDto filter)
    {
        Query.Include(a => a.ActivityType)
             .Include(a => a.Difficulty);
        
        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            Query.Search(a => a.Name, $"%{filter.SearchTerm}%")
                 .Search(a => a.Description, $"%{filter.SearchTerm}%");
        }
        
        if (filter.ActivityTypeId.HasValue)
            Query.Where(a => a.ActivityTypeId == filter.ActivityTypeId.Value);
        
        if (filter.DifficultyId.HasValue)
            Query.Where(a => a.DifficultyId == filter.DifficultyId.Value);
        
        // Pagination
        if (filter.IsPaged)
        {
            Query.Skip(filter.PageIndex * filter.PageSize)
                 .Take(filter.PageSize);
        }
        
        Query.OrderBy(a => a.Name);
    }
}

// Single result with relationships
public class ActivityWithRelatedSpec : Specification<ImprovActivity>, ISingleResultSpecification<ImprovActivity>
{
    public ActivityWithRelatedSpec(int activityId)
    {
        Query.Where(a => a.Id == activityId)
             .Include(a => a.ActivityType)
             .Include(a => a.ActivitySource)
             .Include("ActivityRelationships.RelatedActivity")
             .Include("ActivityRelationships.RelationshipType")
             .AsSplitQuery(); // Prevent cartesian explosion
    }
}
```

**Usage in Services:**
```csharp
public class ActivityService
{
    private readonly IReadRepository<ImprovActivity> _activityRepo;
    
    public async Task<List<ActivityDto>> SearchActivitiesAsync(ActivitiesFilterDto filter)
    {
        var spec = new ActivitiesFilterSpec(filter);
        var activities = await _activityRepo.ListAsync(spec);
        return activities.Select(MapToDto).ToList();
    }
    
    public async Task<int> CountActivitiesAsync(ActivitiesFilterDto filter)
    {
        var spec = new ActivitiesFilterSpec(filter);
        return await _activityRepo.CountAsync(spec);
    }
}
```

**Benefits:**
- ✅ Reusable query logic across services
- ✅ Type-safe queries with compile-time checking
- ✅ Easy to test (mock specifications or use real database)
- ✅ Efficient SQL generation by EF Core
- ✅ Separation of concerns (queries separate from business logic)
- ✅ Supports complex scenarios (includes, pagination, filtering, ordering)
- ✅ No need to write evaluator - built into Ardalis.Specification

**NuGet Packages:**
- `Ardalis.Specification` (core package)
- `Ardalis.Specification.EntityFrameworkCore` (EF Core integration)

---

## 5. Security & Privacy

### 5.1 Security Requirements

**Authentication & Authorization:**
- **Anonymous access allowed** for browsing, searching, and show planning
- **Authentication optional** for standard users (future personalization features)
- **Authentication required** only for admin operations
- ASP.NET Core Identity for user management
- ApplicationUser extends IdentityUser with custom properties
- Role-based access control using IdentityRole:
  - StandardUser role (default for new registrations)
  - Admin role (assigned by super admin)
- Cookie-based authentication (default for Blazor)
- Identity tables managed by Entity Framework Core:
  - AspNetUsers, AspNetRoles, AspNetUserRoles, etc.
- Password requirements configurable via IdentityOptions
- Two-factor authentication support (optional)
- Account lockout after failed login attempts
- Email confirmation for new accounts (optional)
- Admin role assignment controlled via user management interface
- Anonymous users tracked by IP for rate limiting

**API Security:**
- All AI processing server-side (protect API keys)
- Input validation and sanitization
- Rate limiting on video generation endpoints (admin only)
- Rate limiting on show planner (IP-based for anonymous, user-based for authenticated)
- Authorization checks on all admin endpoints
- Endpoint access levels:
  - `/api/activities` (GET) - [AllowAnonymous] - Everyone
  - `/api/activities/{id}` (GET) - [AllowAnonymous] - Everyone
  - `/api/activities` (POST/PUT/DELETE) - [Authorize(Roles = "Admin")] - Admin only
  - `/api/activities/{id}/generate-video` - [Authorize(Roles = "Admin")] - Admin only
  - `/api/activities/{id}/generate-script` - [Authorize(Roles = "Admin")] - Admin only
  - `/api/shows/plan` (POST) - [AllowAnonymous] - Everyone (returns result, doesn't save)
  - `/api/shows` (POST) - [Authorize] - Authenticated users only (saves to database)

**Data Protection:**
- Secure storage of API keys (environment variables, Azure Key Vault)
- HTTPS only
- SQL injection prevention (parameterized queries via EF Core)
- User passwords hashed with ASP.NET Core Identity

### 5.2 Cost Control

- Rate limits on video generation (e.g., 5 videos per user per hour)
- Optional credit system for expensive operations
- Caching of common AI results
- Monitor AI API usage via Aspire telemetry

---

## 6. User Experience

### 6.1 Key User Flows

**Flow 0: Access the Application**

**Anonymous Access (No Login Required):**
1. User navigates to site
2. User immediately sees activity browser
3. User can search, filter, view activities, watch videos
4. User can use show planner (results not saved)
5. Optional: User clicks "Sign In" or "Register" for personalized features

**Authenticated Access (Optional Login):**
1. User clicks "Sign In" or "Register"
2. User enters email and password (or creates account)
3. System authenticates and determines role
4. User returns to activity browser with additional capabilities:
   - StandardUser: Future features (favorites, history, save shows)
   - Admin: Content management, video generation, admin panel

**Flow 1: Browse and Learn About an Activity (Any User - Anonymous or Authenticated)**
1. User searches/filters activities
2. User can filter by type (Game, Technique, Warmup, etc.)
3. User can filter by source (e.g., show only activities from "Truth in Comedy")
4. User clicks on activity card
5. User views activity details, rules, and examples
6. User sees source attribution (e.g., "From: Impro by Keith Johnstone")
7. User sees related activities section with:
   - Aliases ("Also known as...")
   - Variations ("Variations of this activity...")
   - Similar activities ("You might also like...")
8. User clicks on related activity to navigate to it
9. User watches AI-generated demonstration video (if available)
10. User browses external video references (YouTube, etc.)
11. User clicks timestamp links to see specific moments in external videos
12. User reads example scripts

**Flow 2: Generate Video for an Activity (Admin Only)**
1. Admin navigates to activity detail page
2. Admin clicks "Generate Video" button (only visible to admins)
3. System queues generation and establishes SignalR connection
4. Admin receives real-time progress updates (0%, 25%, 50%, 75%, 100%)
5. Admin can navigate away and return - progress persists
6. System pushes completion notification via SignalR
7. All users can now watch the completed video

**Flow 3: Plan an Improv Show (Any User - Anonymous or Authenticated)**
1. User enters list of available players
2. User selects activities from database (typically games)
3. User clicks "Optimize with AI"
4. System generates balanced show order with player assignments
5. User reviews and manually adjusts if needed
6. User prints show cards for host
7. Optional: Authenticated users can click "Save Show" to store in database for later access
8. Anonymous users receive prompt: "Sign in to save your show plan"

**Flow 4: Manage Activities (Admin Only)**
1. Admin navigates to admin panel
2. Admin clicks "Add New Activity"
3. Admin selects activity type (Game, Technique, Warmup, etc.)
4. Admin selects or creates activity source:
   - Search existing sources
   - Or create new source (Book, Website, Workshop, etc.)
   - Enter source details (name, author, year, URL)
5. Admin enters activity details (name, rules, description, player requirements, etc.)
6. Admin optionally enters example script/dialogue
7. Admin saves activity to database
8. Admin adds external video references:
   - Pastes YouTube/Vimeo URL
   - Writes description of how video demonstrates technique
   - Adds timestamps (e.g., "0:45 - First example", "2:30 - Advanced variation")
9. Admin can queue video generation
10. Activity becomes visible to all users with proper source attribution

**Flow 5: Add Video Reference to Existing Game (Admin Only)**
1. Admin views game detail page
2. Admin clicks "Add Video Reference"
3. Admin enters video URL (YouTube, Vimeo, etc.)
4. Admin writes description explaining relevance
5. Admin adds timestamps:
   - Enter time in seconds or MM:SS format
   - Add label and description for each timestamp
6. Admin saves video reference
7. Reference appears for all users with clickable timestamps

**Flow 6: Link Related Activities (Admin Only)**
1. Admin views activity detail page
2. Admin clicks "Add Related Activity"
3. Admin searches for and selects another activity
4. Admin chooses relationship type:
   - **Alias:** Same activity with different name (e.g., "Zip Zap Zop" and "Whoosh Bang Pow")
   - **Variation:** Modified version (e.g., "Freeze Tag" and "Freeze Tag with Emotions")
   - **Similar:** Related activity (e.g., "Yes, And" technique and "Yes, But" exercise)
5. Admin optionally adds notes explaining the relationship
6. Admin saves relationship
7. Relationship appears on both activities' detail pages
8. Users can click through to explore related activities

**Flow 7: Manage Activity Sources (Admin Only)**
1. Admin navigates to "Sources" management page
2. Admin views list of all activity sources
3. Admin can add new source:
   - Select source type (Book, Website, Workshop, Class, Person, Original)
   - Enter name (e.g., "Truth in Comedy", "improvencyclopedia.org")
   - Enter author/creator if applicable
   - Enter URL if available
   - For books: Enter ISBN and/or Amazon affiliate link
   - Enter publication year if applicable
   - Add description/notes
4. Admin can edit existing sources
5. Admin can view which activities are associated with each source
6. Admin can merge duplicate sources if needed
7. System can auto-generate affiliate links from ISBN (if configured)

**Flow 8: Voice Mode Activity Capture (Admin Only)**
1. Admin navigates to activity creation
2. Admin clicks "Voice Mode" button
3. System prompts: "Describe the activity you want to add"
4. Admin speaks naturally: "This is a warmup called Zip Zap Zop. Players stand in a circle. The first player points at someone and says Zip. That person points at another and says Zap. Continue with Zop, then repeat. It's for 3 or more players and takes about 5 minutes."
5. System uses speech-to-text (Whisper/Azure Speech)
6. AI (Semantic Kernel) parses the speech and extracts:
   - Activity name: "Zip Zap Zop"
   - Activity type: "Warmup"
   - Rules: Auto-formatted from description
   - Player count: 3+
   - Duration: 5 minutes
7. System displays parsed fields for admin review/edit
8. Admin can:
   - Accept and save
   - Speak additional details ("Add rule: Players should make eye contact")
   - Manually edit any field
   - Re-record if needed
9. Activity saved to database with proper formatting

### 6.2 Performance Requirements

- Activity search results: < 500ms
- Page load times: < 2s
- Video generation: 5-10 minutes (background with real-time updates)
- Script generation: < 30s
- Show optimization: < 10s
- SignalR message latency: < 100ms
- Video progress updates: Pushed immediately (no polling delays)

---

## 7. Testing Strategy

### 7.1 Testing Philosophy

**Test-Driven Development (TDD) Approach:**
- Write tests before or alongside implementation
- Tests serve as living documentation
- Red-Green-Refactor cycle
- Minimum 80% code coverage target

**Testing Pyramid:**
```
        /\        E2E Tests (Few)
       /  \       - Critical user journeys
      /____\      - Smoke tests
     /      \     
    / Integr \    Integration Tests (Some)
   /  ation   \   - API endpoints
  /____________\  - Database operations
 /              \ - SignalR hubs
/   Unit Tests   \ Unit Tests (Many)
\________________/ - Business logic
                   - Services
                   - Utilities
```

### 7.2 Test Types

**Unit Tests:**
- Test individual methods and classes in isolation
- Fast execution (milliseconds)
- No external dependencies (use mocks)
- Run on every build

**Integration Tests:**
- Test components working together
- Use Testcontainers for PostgreSQL
- Test actual database operations
- Test SignalR communication
- Test Semantic Kernel integrations
- Test specifications with real database queries

**End-to-End Tests:**
- Test critical user journeys
- Playwright for Blazor UI testing
- Test authentication flows
- Test video generation workflow (with mocked AI)

### 7.3 Test Examples

**Unit Test Example:**
```csharp
public class ActivityServiceTests
{
    [Fact]
    public async Task GetActivityById_ReturnsActivity_WhenExists()
    {
        // Arrange
        var mockRepo = new Mock<IActivityRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(new ImprovActivity { Id = 1, Name = "Zip Zap Zop" });
        
        var service = new ActivityService(mockRepo.Object);
        
        // Act
        var result = await service.GetActivityByIdAsync(1);
        
        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Zip Zap Zop");
    }
}
```

**Integration Test Example:**
```csharp
public class ActivitiesApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly PostgreSqlContainer _postgres;
    
    [Fact]
    public async Task GetActivities_ReturnsOk_WithActivities()
    {
        // Arrange
        await SeedDatabaseAsync();
        
        // Act
        var response = await _client.GetAsync("/api/activities");
        var activities = await response.Content.ReadFromJsonAsync<List<ActivityDto>>();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        activities.Should().HaveCountGreaterThan(0);
    }
}
```

**Specification Test Example:**
```csharp
public class ActivitySpecificationTests
{
    [Fact]
    public async Task ActivitiesFilterSpec_WithSearchTerm_ReturnsMatchingActivities()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ImprovDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var context = new ImprovDbContext(options);
        var repo = new ReadRepository<ImprovActivity>(context);
        
        await SeedTestDataAsync(context);
        
        var filter = new ActivitiesFilterDto { SearchTerm = "Zip" };
        var spec = new ActivitiesFilterSpec(filter);
        
        // Act
        var results = await repo.ListAsync(spec);
        
        // Assert
        results.Should().HaveCount(1);
        results[0].Name.Should().Contain("Zip");
        results[0].ActivityType.Should().NotBeNull(); // Verify include worked
    }
}
```

**E2E Test Example:**
```csharp
public class ActivityBrowsingTests
{
    [Fact]
    public async Task User_CanSearchAndViewActivity()
    {
        // Arrange
        await using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync();
        var page = await browser.NewPageAsync();
        
        // Act
        await page.GotoAsync("https://localhost:5001");
        await page.FillAsync("input[placeholder='Search activities']", "Zip Zap");
        await page.ClickAsync("text=Zip Zap Zop");
        
        // Assert
        await Expect(page.Locator("h1")).ToContainTextAsync("Zip Zap Zop");
        await Expect(page.Locator(".activity-description")).ToBeVisibleAsync();
    }
}
```

### 7.4 CI/CD Integration

**Automated Testing in Pipeline:**
1. **On Pull Request:**
   - Run all unit tests
   - Run integration tests
   - Check code coverage (fail if < 80%)
   - Report results in PR comments

2. **On Merge to Main:**
   - Run full test suite
   - Run E2E tests
   - Generate coverage report
   - Deploy to staging if all tests pass

3. **Before Production Deploy:**
   - Run smoke tests on staging
   - Manual approval required
   - Deploy to production
   - Run smoke tests on production

### 7.5 Test Data Management

**Test Database:**
- Use Testcontainers for isolated PostgreSQL instances
- Each test gets fresh database
- Seed common test data via fixtures
- Use Respawn to clean database between tests

**Test AI Services:**
- Mock OpenAI/Semantic Kernel responses
- Use test API keys in non-production
- Stub video generation (return mock URLs)
- Optional: Record real API responses for replay

### 7.6 Testing Guidelines

**DO:**
- ✅ Write tests for all business logic
- ✅ Test edge cases and error handling
- ✅ Use descriptive test names (Given_When_Then pattern)
- ✅ Keep tests independent and isolated
- ✅ Test authentication and authorization
- ✅ Run tests before committing

**DON'T:**
- ❌ Test framework code (EF Core, ASP.NET)
- ❌ Skip tests to "move faster"
- ❌ Write flaky tests that randomly fail
- ❌ Test implementation details
- ❌ Share state between tests

### 7.7 Code Coverage Goals

**Minimum Coverage by Layer:**
- Business Logic: 90%
- API Controllers: 80%
- Services: 85%
- Repositories: 70% (mostly integration tests)
- Overall: 80%

**Coverage Reports:**
- Generated on every CI build
- Published to Azure DevOps or GitHub
- Tracked over time to prevent regression

---

## 8. Deployment & Containerization

### 7.1 Container Strategy

**Development:**
- .NET Aspire orchestrates all containers locally
- Docker Compose for non-Aspire scenarios
- Hot reload support for rapid development

**Production Deployment Options:**

**Option 1: Azure Container Apps (Recommended)**
- Aspire deploys directly to Azure Container Apps
- Automatic scaling based on load
- Managed PostgreSQL (Azure Database for PostgreSQL)
- Built-in monitoring and logging

**Option 2: Docker Compose**
- Simple VM deployment
- Docker Compose for multi-container orchestration
- Suitable for small-to-medium traffic
- Manual scaling

**Option 3: Kubernetes**
- Full container orchestration
- Horizontal pod autoscaling
- Best for high-traffic scenarios
- Azure Kubernetes Service (AKS) or self-hosted

**Option 4: Azure App Service**
- Deploy containers to App Service
- Easy scaling and management
- Good middle ground between simplicity and power

### 7.2 Container Images

**API Container:**
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["ImprovByExample.Api/ImprovByExample.Api.csproj", "ImprovByExample.Api/"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ImprovByExample.Api.dll"]
```

**Blazor Web Container:**
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["ImprovByExample.Web/ImprovByExample.Web.csproj", "ImprovByExample.Web/"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ImprovByExample.Web.dll"]
```

### 7.3 Environment Configuration

**Environment Variables:**
- `ConnectionStrings__PostgreSQL` - Database connection
- `OpenAI__ApiKey` - OpenAI API key
- `VideoGeneration__ApiKey` - Video generation API key
- `ASPNETCORE_ENVIRONMENT` - Development/Staging/Production
- `SignalR__Backplane` (if using multiple instances)

**Secrets Management:**
- Development: User secrets / .env files
- Production: Azure Key Vault or Kubernetes secrets

### 7.4 CI/CD Pipeline

**GitHub Actions Example:**
```yaml
name: Build and Deploy

on:
  push:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Build API Image
      run: docker build -t improvbyexample/api:latest -f Dockerfile.api .
    
    - name: Build Web Image
      run: docker build -t improvbyexample/web:latest -f Dockerfile.web .
    
    - name: Push to Registry
      run: |
        docker push improvbyexample/api:latest
        docker push improvbyexample/web:latest
    
    - name: Deploy to Azure Container Apps
      run: az containerapp update --name improvbyexample-api --image improvbyexample/api:latest
```

---

## 9. Development Phases

This section covers both the **MVP Implementation Phases** (detailed step-by-step foundation setup) and the **Feature Phases** (major functionality milestones).

---

## 9.1 MVP Implementation Phases

These are detailed technical implementation phases for building the foundation (MVP). Each phase builds upon the previous one.

### MVP Phase 1: Blazor App Foundation ✅ COMPLETED
**Status:** ✅ Completed (January 2026)  
**Goal:** Create a working Blazor Web App with MudBlazor UI that runs successfully

**Steps:**
1. Run `dotnet new blazor -n ImprovByExample.Web -o src/ImprovByExample.Web`
2. Verify the template app runs correctly
3. Install MudBlazor package
4. Configure MudBlazor in Program.cs:
   - Add `builder.Services.AddMudServices();`
   - Add `MapRazorComponents<App>().AddInteractiveServerComponents().AddAdditionalAssemblies(...)`
5. Add MudBlazor to _Imports.razor: `@using MudBlazor`
6. Update App.razor:
   - Add MudBlazor CSS/JS references
   - Use proper render modes (`@rendermode="@RenderMode.InteractiveServer"`)
7. Update MainLayout.razor with MudBlazor components:
   - MudThemeProvider with light/dark mode toggle
   - MudAppBar with menu and branding
   - MudDrawer for navigation
   - MudMainContent for page content
8. Test home page renders with MudBlazor UI
9. Create basic navigation menu
10. Verify all MudBlazor components work (buttons, cards, dialogs)

**Deliverables:**
- Working Blazor Web App on http://localhost:5000
- MudBlazor integrated and rendering correctly
- Navigation and layout functioning
- Light/dark theme toggle working

**Success Criteria:**
- App starts without errors
- Home page displays properly
- Can navigate between pages
- All static resources load (CSS/JS)

---

### MVP Phase 2: Clean Architecture & Domain Layer ✅ COMPLETED
**Status:** ✅ Completed (January 2026)  
**Goal:** Add Clean Architecture structure and domain models

**Steps:**
1. Create solution and projects:
   - `dotnet new sln -n ImprovByExample`
   - `dotnet new classlib -n ImprovByExample.Domain -o src/ImprovByExample.Domain`
   - `dotnet new classlib -n ImprovByExample.Application -o src/ImprovByExample.Application`
   - `dotnet new classlib -n ImprovByExample.Infrastructure -o src/ImprovByExample.Infrastructure`
   - `dotnet new webapi -n ImprovByExample.Api -o src/ImprovByExample.Api`
   - Add all projects to solution
2. Set up project references (Application → Domain, Infrastructure → Application, Api/Web → Application)
3. Create domain entities in Domain project:
   - ImprovActivity
   - ActivityType
   - ActivitySource
   - Difficulty
   - RelationshipType
   - VideoGenerationStatus
   - All other lookup entities
4. Create base entity classes with audit fields
5. Install xUnit in test project
6. Write unit tests for domain models
7. Verify all tests pass

**Deliverables:**
- Clean Architecture project structure
- All domain entities created
- Unit test project with passing tests
- Project references configured

**Success Criteria:**
- Solution builds successfully
- Domain models have proper properties and relationships
- Unit tests pass (green)

---

### MVP Phase 3: Database & EF Core Setup ✅ COMPLETED
**Status:** ✅ Completed (January 2026)  
**Goal:** Set up PostgreSQL database with Entity Framework Core

**Steps:**
1. Start PostgreSQL in Docker:
   ```bash
   docker run --name improvbyexample-postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres:16
   ```
2. Install EF Core packages:
   - Microsoft.EntityFrameworkCore (Infrastructure)
   - Npgsql.EntityFrameworkCore.PostgreSQL (Infrastructure)
   - Microsoft.EntityFrameworkCore.Design (Infrastructure)
3. Create ImprovDbContext in Infrastructure
4. Configure DbSets for all entities
5. Configure entity relationships using Fluent API
6. Install `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`
7. Create initial migration: `dotnet ef migrations add InitialCreate`
8. Apply migration: `dotnet ef database update`
9. Verify database tables created in PostgreSQL

**Deliverables:**
- PostgreSQL container running
- ImprovDbContext configured
- Initial migration created and applied
- Database tables created

**Success Criteria:**
- Can connect to PostgreSQL
- Migrations run successfully
- All tables exist in database
- Relationships configured correctly

---

### MVP Phase 4: Repository Pattern & Specifications ✅ COMPLETED
**Status:** ✅ Completed (January 2026)  
**Goal:** Implement Repository Pattern with Ardalis.Specification

**Steps:**
1. Install Ardalis.Specification packages in Application and Infrastructure
2. Create IRepository<T> and IReadRepository<T> interfaces in Application
3. Implement repositories in Infrastructure using EF Core
4. Create base specifications (e.g., GetByIdSpec, GetAllSpec)
5. Create activity-specific specifications:
   - ActivitiesFilterSpec (search, filter by type/source/difficulty)
   - ActivitiesWithIncludesSpec (eager loading relationships)
6. Write unit tests for specifications
7. Set up Testcontainers for integration tests
8. Write integration tests for repository operations
9. Verify all tests pass

**Deliverables:**
- Repository interfaces and implementations
- Specification classes for queries
- Unit tests for specifications
- Integration tests with Testcontainers

**Success Criteria:**
- Can query database using specifications
- Filtering and searching works correctly
- All tests pass (unit + integration)

---

### MVP Phase 5: Identity & Authentication ✅ COMPLETED
**Status:** ✅ Completed (January 2026)  
**Goal:** Add ASP.NET Core Identity with roles and authorization

**Steps:**
1. Install Microsoft.AspNetCore.Identity.EntityFrameworkCore
2. Create ApplicationUser class extending IdentityUser
3. Update ImprovDbContext to inherit from IdentityDbContext<ApplicationUser>
4. Configure Identity in Program.cs (password requirements, lockout)
5. Create and apply Identity migration
6. Create data seeder for roles (Admin, StandardUser)
7. Seed initial admin user (admin@improvbyexample.com)
8. Add CreatedById/UpdatedById to all entities
9. Configure DeleteBehavior.Restrict on relationships
10. Write integration tests for authentication
11. Configure IHttpContextAccessor for current user access

**Deliverables:**
- Identity tables in database
- Admin and StandardUser roles
- Initial admin user seeded
- Audit fields on all entities
- Authentication tests

**Success Criteria:**
- Can create and authenticate users
- Roles assigned correctly
- Admin user can log in
- Audit fields populated on entity changes

---

### MVP Phase 6: API Layer with Authorization ✅ COMPLETED
**Status:** ✅ Completed (January 2026)  
**Goal:** Create REST API with role-based authorization

**Steps:**
1. Configure authorization policies in Api Program.cs ✅
2. Create DTOs in Application layer (CreateActivityDto, UpdateActivityDto, ActivityDto) ✅
3. Install and configure FluentValidation ✅
4. Create validators for DTOs ✅
5. Write unit tests for validators ✅
6. Create ActivitiesController with CRUD endpoints ✅
7. Apply authorization attributes: ✅
   - `[AllowAnonymous]` on GET endpoints
   - `[Authorize(Roles = "Admin")]` on POST/PUT/DELETE
8. Implement pagination with PagedResult<T> ✅
9. Install Scalar.AspNetCore for API documentation ✅
10. Configure OpenAPI metadata and Scalar UI endpoint (/scalar/v1) ✅
11. Add XML documentation comments to controllers ✅
12. Write integration tests for API endpoints ⬜ (deferred to Phase 9)
13. Test anonymous and authenticated access ⬜ (deferred to Phase 9)

**Deliverables:**
- ActivitiesController with full CRUD ✅
- DTOs and validators ✅
- Anonymous read, admin write authorization ✅
- Scalar API documentation UI ✅
- ActivityService with specifications for querying ✅
- 7 passing unit tests for validators ✅

**Success Criteria:**
- API endpoints work correctly ✅
- Anonymous users can read activities ✅
- Only admins can modify activities ✅
- Validation errors returned properly ✅
- Unit tests pass ✅

**Note:** Integration tests and end-to-end testing of authentication/authorization will be completed in MVP Phase 9 (Data Seeding & Testing).

---

### MVP Phase 7: Structured Logging ✅ COMPLETED
**Status:** ✅ Completed (January 2026)  
**Goal:** Add structured logging with Serilog

**Steps:**
1. Install Serilog packages (AspNetCore, Console, File, enrichers) ✅
2. Configure Serilog in Program.cs (Api and Web) ✅
3. Add enrichers (Environment, Thread, Machine, Process) ✅
4. Configure log levels per namespace ✅
5. Add structured logging throughout application layers ✅
6. Test logging output to console and file ✅
7. Optional: Set up Seq container for log aggregation ⬜ (deferred)
8. Write tests that verify logging behavior ✅

**Deliverables:**
- Serilog configured with Console and File sinks ✅
- Structured logging in ActivityService and ActivitiesController ✅
- Log enrichers configured (Environment, Machine, Thread, Process) ✅
- Logging tests (4 tests in ActivityServiceLoggingTests) ✅
- Documentation (LOGGING.md) ✅

**Success Criteria:**
- Logs appear in console and file with proper structure ✅
- Enriched data captured in logs (environment, thread, etc.) ✅
- Log levels configured appropriately per namespace ✅
- Can filter and search logs effectively ✅
- All 12 unit tests passing ✅

---

### MVP Phase 8: Blazor UI Pages ⬜ NOT STARTED
**Status:** ⬜ Not Started (Basic pages exist but no API integration)  
**Goal:** Build Blazor pages for browsing and managing activities

**Steps:**
1. Update Web project to reference Application and Infrastructure
2. Configure HttpClient in Web Program.cs to call API
3. Create Activities browse page:
   - Display activities in MudDataGrid or MudCards
   - Add search functionality
   - Add filters (type, source, difficulty)
   - Show pagination
4. Create Activity detail page
5. Create Admin pages for CRUD:
   - Add Activity form
   - Edit Activity form
   - Delete confirmation dialog
6. Create Sources management pages
7. Add navigation menu links
8. Test all pages work correctly
9. Write component tests with bUnit (if time permits)

**Deliverables:**
- Activities browse page with search/filter
- Activity detail page
- Admin CRUD pages
- Sources management pages
- Navigation working

**Success Criteria:**
- Can browse and search activities
- Can view activity details
- Admins can add/edit/delete activities
- All pages render correctly with MudBlazor

---

### MVP Phase 8.5: Authentication Implementation ⬜ NOT STARTED
**Status:** ⬜ Not Started (Currently using temporary workaround)  
**Goal:** Implement complete user authentication flow for Web and API

**Current State:**
- ✅ ASP.NET Core Identity configured (Phase 5)
- ✅ Roles and admin user seeded
- ⚠️ **Temporary workaround in place:** API controllers use `[AllowAnonymous]` and default to "system" user ID
- ❌ No login/registration UI
- ❌ No authentication between Web and API

**Authentication Checklist:**

**1. Remove Temporary Workarounds** ⬜
   - [ ] Remove `[AllowAnonymous]` from Create/Update/Delete endpoints in ActivitiesController.cs
   - [ ] Restore `[Authorize(Roles = "Admin")]` attributes
   - [ ] Remove `?? "system"` fallback in userId checks (lines 111, 151 in ActivitiesController.cs)
   - [ ] Remove temporary logging statements for system user
   - [ ] Update all other controllers that may have similar workarounds

**2. Configure Cookie Authentication Between API and Web** ⬜
   - [ ] Install Microsoft.AspNetCore.Authentication.Cookies in both API and Web projects
   - [ ] Configure cookie authentication in API Program.cs:
     ```csharp
     builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
         .AddCookie(options =>
         {
             options.Cookie.Name = ".ImprovByExample.Auth";
             options.Cookie.HttpOnly = true;
             options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
             options.ExpireTimeSpan = TimeSpan.FromHours(24);
             options.SlidingExpiration = true;
             options.Events.OnRedirectToLogin = context =>
             {
                 context.Response.StatusCode = 401;
                 return Task.CompletedTask;
             };
         });
     ```
   - [ ] Configure cookie authentication in Web Program.cs (similar configuration)
   - [ ] Ensure both apps use the same cookie name and settings
   - [ ] Add `app.UseAuthentication()` and `app.UseAuthorization()` to both middleware pipelines

**3. Create Authentication API Endpoints** ⬜
   - [ ] Create AuthController.cs in API project
   - [ ] Create RegisterDto (Email, Password, ConfirmPassword, FirstName, LastName)
   - [ ] Create LoginDto (Email, Password, RememberMe)
   - [ ] Create RegisterDto and LoginDto validators with FluentValidation
   - [ ] Implement POST /api/auth/register endpoint:
     - [ ] Validate input
     - [ ] Create user with UserManager
     - [ ] Assign "StandardUser" role by default
     - [ ] Sign in user and return success
   - [ ] Implement POST /api/auth/login endpoint:
     - [ ] Validate credentials with SignInManager
     - [ ] Set authentication cookie
     - [ ] Return user info (Id, Email, Roles)
   - [ ] Implement POST /api/auth/logout endpoint:
     - [ ] Clear authentication cookie
     - [ ] Sign out user
   - [ ] Implement GET /api/auth/user endpoint:
     - [ ] Return current authenticated user info
     - [ ] Return 401 if not authenticated
   - [ ] Write unit tests for Auth validators
   - [ ] Write integration tests for Auth endpoints

**4. Create Authentication Service in Web App** ⬜
   - [ ] Create IAuthService interface in Web/Services
   - [ ] Create AuthService implementation:
     - [ ] RegisterAsync(RegisterDto) → calls API /api/auth/register
     - [ ] LoginAsync(LoginDto) → calls API /api/auth/login
     - [ ] LogoutAsync() → calls API /api/auth/logout
     - [ ] GetCurrentUserAsync() → calls API /api/auth/user
   - [ ] Register AuthService in Web Program.cs DI container
   - [ ] Configure HttpClient to include cookies with API calls

**5. Create Login Page (Blazor)** ⬜
   - [ ] Create Components/Pages/Login.razor
   - [ ] Add MudForm with email and password fields
   - [ ] Add "Remember Me" checkbox
   - [ ] Add validation using FluentValidation
   - [ ] Call AuthService.LoginAsync() on submit
   - [ ] Redirect to home page on success
   - [ ] Display error messages on failure
   - [ ] Add "Forgot Password?" link (placeholder for future)
   - [ ] Add "Register" link to registration page
   - [ ] Style with MudBlazor components

**6. Create Registration Page (Blazor)** ⬜
   - [ ] Create Components/Pages/Register.razor
   - [ ] Add MudForm with fields: Email, Password, Confirm Password, First Name, Last Name
   - [ ] Add client-side validation
   - [ ] Call AuthService.RegisterAsync() on submit
   - [ ] Auto-login after successful registration
   - [ ] Redirect to home page
   - [ ] Display error messages (email already exists, password requirements, etc.)
   - [ ] Add "Already have an account? Login" link
   - [ ] Style with MudBlazor components

**7. Add Authentication UI Components** ⬜
   - [ ] Create LoginDisplay.razor component for MainLayout
   - [ ] Show "Login" and "Register" buttons when not authenticated
   - [ ] Show user name and "Logout" button when authenticated
   - [ ] Add user menu dropdown with:
     - [ ] Profile link (future)
     - [ ] Logout button
   - [ ] Update MainLayout.razor to include LoginDisplay
   - [ ] Add AuthorizeView components to conditionally show admin features
   - [ ] Protect admin routes (redirect to login if not authenticated)

**8. Implement Current User Context** ⬜
   - [ ] Create ICurrentUserService interface in Application
   - [ ] Implement CurrentUserService in Infrastructure:
     - [ ] Inject IHttpContextAccessor
     - [ ] Read UserId from User.FindFirst(ClaimTypes.NameIdentifier)
     - [ ] Cache user info for request lifetime
   - [ ] Register CurrentUserService in API Program.cs
   - [ ] Update services to use ICurrentUserService instead of direct User access
   - [ ] Update audit fields to use ICurrentUserService

**9. Update Activity Management for Authenticated Users** ⬜
   - [ ] Update ApiClient in Web to handle 401 responses
   - [ ] Redirect to login page on 401
   - [ ] Show "Login Required" message for admin actions
   - [ ] Hide "Add Activity" button if not authenticated as admin
   - [ ] Test activity creation with authenticated admin user
   - [ ] Test activity creation blocked for non-admin users

**10. Testing** ⬜
   - [ ] Write unit tests for AuthController
   - [ ] Write integration tests for authentication flow:
     - [ ] Register new user → Login → Access protected endpoint
     - [ ] Login with invalid credentials → Expect 401
     - [ ] Access admin endpoint as StandardUser → Expect 403
     - [ ] Access admin endpoint as Admin → Expect success
   - [ ] Manual testing of full authentication flow
   - [ ] Test cookie expiration and renewal
   - [ ] Test logout clears authentication

**11. Security Hardening** ⬜
   - [ ] Enable HTTPS redirect in production
   - [ ] Configure CORS properly if Web and API on different domains
   - [ ] Add anti-forgery token support
   - [ ] Configure rate limiting on auth endpoints (prevent brute force)
   - [ ] Add account lockout after failed login attempts (already configured in Identity)
   - [ ] Log authentication events (login, logout, failed attempts)

**Deliverables:**
- Authentication API endpoints (register, login, logout, get user)
- AuthService for Web app
- Login and Registration Blazor pages
- LoginDisplay component in MainLayout
- Current user context throughout application
- All temporary workarounds removed
- Comprehensive authentication tests
- Security hardening measures

**Success Criteria:**
- ✅ Users can register new accounts
- ✅ Users can login with email/password
- ✅ Authentication cookies work between Web and API
- ✅ Admin users can create/edit/delete activities
- ✅ Non-admin users cannot access admin endpoints (403 Forbidden)
- ✅ Unauthenticated users are redirected to login for protected actions
- ✅ All tests pass
- ✅ No temporary workarounds remain in code

---

### MVP Phase 9: Data Seeding & Testing 🔄 PARTIALLY COMPLETED
**Status:** 🔄 Partially Completed (Seeding done, comprehensive testing needed)  
**Goal:** Seed initial data and complete testing

**Steps:**
1. Create data seeder in Infrastructure
2. Seed source types (Book, Website, Workshop, Class, Person, Original)
3. Seed video platforms (YouTube, Vimeo, Other)
4. Seed activity types (Game, Warmup, Technique, Exercise)
5. Seed difficulty levels (Beginner, Intermediate, Advanced)
6. Seed relationship types (Alias, Variation, Similar)
7. Seed 4+ activity sources (improv books and websites)
8. Seed 10-20 activities across different types
9. Write comprehensive unit tests (target 80% coverage)
10. Write integration tests for all API endpoints
11. Write E2E tests for critical user flows (if time permits)
12. Set up code coverage reporting
13. Fix any failing tests

**Deliverables:**
- Database seeded with initial data
- 6 source types (Book, Website, Workshop, Class, Person, Original)
- 3 video platforms (YouTube, Vimeo, Other)
- 4 activity types (Game, Warmup, Technique, Exercise)
- 3 difficulty levels (Beginner, Intermediate, Advanced)
- 3 relationship types (Alias, Variation, Similar)
- 10-20 activities across types
- 4+ sources (books and websites)
- Comprehensive test suite
- Code coverage report

**Success Criteria:**
- Database has realistic seed data with all lookup tables populated
- All tests pass (unit + integration)
- Code coverage ≥ 80%
- Application ready for Phase 2 features

---

### MVP Phase 10: Deployment & CI/CD ⬜ OPTIONAL
**Status:** ⬜ Not Started (Optional for MVP)  
**Goal:** Containerize and deploy the application

**Steps:**
1. Create Dockerfile for API
2. Create Dockerfile for Web
3. Create docker-compose.yml for local development
4. Test containers run locally
5. Set up GitHub Actions workflow
6. Configure automated testing in CI
7. Set up container registry (Docker Hub or Azure ACR)
8. Deploy to Azure Container Apps (or preferred platform)
9. Configure environment variables and secrets
10. Set up monitoring and health checks

**Deliverables:**
- Dockerfiles for API and Web
- docker-compose.yml
- CI/CD pipeline
- Deployed application
- Monitoring configured

**Success Criteria:**
- Containers build and run successfully
- CI pipeline runs tests on every commit
- Application deployed and accessible
- Health checks passing

---

## 9.2 Feature Phases

These are the major feature milestone phases for building out the product functionality.

---

## 10. Future Enhancements

### Phase 1: Foundation (MVP)
**Status:** ✅ COMPLETE

This phase establishes the foundation with:
- Blazor Web App with MudBlazor UI
- Clean Architecture structure
- PostgreSQL database with Entity Framework Core
- Repository pattern with Ardalis.Specification
- ASP.NET Core Identity with roles
- Initial data seeding

See **MVP Implementation Phases 1-9** above for detailed steps.

---

### Phase 2: External References & Activity Relationships
**Status:** 🚧 IN PROGRESS

**Goal:** Add support for external video references and activity relationships to enrich the activity database.

**Features:**
- External video reference management (CRUD)
  - Support for YouTube, Vimeo, and other video platforms
  - Video descriptions explaining how they demonstrate activities
  - Admin interface for adding/editing video references
  
- Video timestamp functionality
  - Add timestamps to videos marking specific moments
  - Label and describe each timestamp
  - Clickable timestamps that jump to specific moments in videos
  
- Activity relationship management
  - Link activities as Aliases (same activity, different name)
  - Link activities as Variations (modified versions)
  - Link activities as Similar (related activities)
  - Bidirectional relationship display
  - Admin interface for creating and managing relationships
  
- User interface enhancements
  - Display external video references on activity detail pages
  - YouTube/Vimeo embed integration with timestamp support
  - Related activities section showing aliases, variations, and similar activities
  - Click-through navigation between related activities

**Implementation Steps:**
1. Create API endpoints for ExternalVideoReference CRUD operations
2. Create API endpoints for ActivityRelationship CRUD operations  
3. Add FluentValidation validators for video references and relationships
4. Build Blazor UI components for managing video references
5. Build Blazor UI for managing activity relationships
6. Implement YouTube/Vimeo embed components with timestamp support
7. Add related activities display to activity detail page
8. Write unit and integration tests

**Success Criteria:**
- Admins can add external video references with timestamps to activities
- Users can click timestamps to jump to specific moments in videos
- Admins can link activities together with relationship types
- Users can browse related activities and navigate between them
- All CRUD operations have appropriate authorization
- Tests pass with good coverage

---

### Phase 3: AI Video Generation
**Status:** ⬜ NOT STARTED

**Goal:** Generate AI-powered demonstration videos for activities

**Features:**
- Background service implementation with unit tests for background service logic
- SignalR hub configuration with integration tests for SignalR hub
- Real-time progress updates via SignalR with tests for progress tracking and state management
- Video generation API integration with unit tests using mocked video API
- Progress tracking in database
- Video display in UI
- Error handling and retries with tests for retry logic and error scenarios
- SignalR reconnection handling
- E2E tests for video generation workflow
- Load tests for concurrent video generation

**Success Criteria:**
- Admins can request video generation for activities
- Real-time progress updates work via SignalR
- Generated videos display correctly in UI
- Error handling and retry logic tested
- All tests pass with good coverage

---

### Phase 4: Show Planner
**Status:** ⬜ NOT STARTED

**Goal:** AI-powered show planning and player assignment

**Features:**
- Show planner data model with unit tests for data model validation
- Player assignment logic with unit tests for player distribution algorithm
- AI optimization integration with tests using mocked AI optimization
- Show planner UI with component tests for show planner UI
- Printable show cards
- E2E tests for show planning workflow
- Tests for constraint validation (player wait times, etc.)

**Success Criteria:**
- Users can create optimized show plans
- Player distribution algorithm works correctly
- Show cards can be printed
- All constraints validated properly
- Tests pass with good coverage

---

### Phase 5: Polish & Launch
**Status:** ⬜ NOT STARTED

**Goal:** Production-ready application with full polish

**Features:**
- UI/UX improvements
- Performance optimization with performance tests and benchmarks
- Upgrade to .NET 12 LTS (when released in Q4 2026)
- User authentication UI (login/registration)
- Admin functions and dashboards
- Documentation
- Full regression test suite
- E2E smoke tests for critical paths
- Security testing (penetration testing, vulnerability scanning)
- Production deployment (Azure Container Apps or preferred platform)
- Monitoring and alerting setup
- Synthetic monitoring and health checks

**Success Criteria:**
- Application is production-ready
- All security testing passed
- Performance benchmarks met
- Comprehensive documentation complete
- Deployed to production

---

### Phase 6: Commerce Features (Future)
**Status:** ⬜ FUTURE

**Features:**
- Class management and booking system
- Industrial/event booking system
- Payment processing integration
- Calendar integration
- Customer relationship management
- Booking confirmations and reminders

---

### Phase 7: Marketing Automation (Future)
**Status:** ⬜ FUTURE

**Features:**
- Social media platform integrations (APIs)
- Content scheduling system
- Duplicate tracking database
- Social media post queue management
- Analytics and reporting dashboard
- Caption and hashtag generation (AI-powered)
- Platform-specific video formatting
- Engagement tracking and optimization

---

## 10. Future Enhancements

### Content & Community
- User accounts and favorites
- Community contributions (user-submitted activities)
- Community-submitted video references (with moderation)
- Automatic YouTube search for activity demonstrations
- Video quality selection (fast/standard/high quality)
- Multiple video styles per activity
- Learning paths organized by activity type
- Rating and review system
- Social sharing features
- Export show plans to PDF/calendar
- Automatic timestamp detection using AI (analyzing video content)
- AI-suggested activity relationships based on rules/descriptions
- Graph visualization of related activities
- "Activity family trees" showing evolution of variations
- Curated collections (e.g., "Best Warmups for Beginners", "Advanced Techniques")
- Source statistics (most referenced books, websites)
- "Explore by source" browsing experience
- ISBN lookup for book sources (auto-fill details)
- Bibliography generation for workshop materials
- **Amazon Associates integration** (auto-generate affiliate links from ISBN)
- **Bookshop.org integration** (support independent bookstores)
- Affiliate revenue dashboard and reporting
- Recommended reading lists with affiliate links
- "Essential improv library" curated collections
- Price comparison across book retailers

### Voice & Accessibility Features
- **Voice mode for activity creation:**
  - Speech-to-text integration (Azure Speech Services/Whisper)
  - Natural language processing to parse activity details
  - Hands-free activity entry
  - Voice commands for navigation
  - Edit activities by voice
- **Voice mode for show planning:**
  - Speak player names instead of typing
  - Voice commands to add/remove activities
  - Voice-activated show card printing
- **Accessibility enhancements:**
  - Screen reader optimization
  - Keyboard navigation throughout app
  - High contrast mode
  - Text-to-speech for activity descriptions
  - Closed captions for generated videos
- **Mobile voice integration:**
  - Quick capture of activities on-the-go
  - Voice notes attached to activities
  - Audio feedback during show planning

### AI-Generated Scripts (Future)
- **Automatic script generation using AI:**
  - Semantic Kernel integration with OpenAI/Azure OpenAI
  - Generate example scripts from activity rules and description
  - Support multiple script variations per activity
  - Admin can regenerate or refine AI-generated scripts
  - Script templates based on activity type
  - One-click generation for activities without scripts
  - Batch script generation for multiple activities
  - Save generated scripts to activity or as separate variations

### Commerce Features
- **Improv Class Sales:**
  - Class catalog with descriptions, schedules, and pricing
  - Online booking and registration
  - Class capacity management
  - Student rosters and attendance tracking
  - Multi-session class packages
  - Early bird discounts and promotional codes
  - Waitlist management
  
- **Industrial/Event Bookings:**
  - Corporate event inquiry forms
  - Custom performance quotes
  - Performer availability calendar
  - Event type selection (team building, entertainment, training)
  - Booking deposits and payment schedules
  - Contract generation and e-signatures
  - Event coordination tools
  
- **Payment & Financial:**
  - Payment gateway integration (Stripe, PayPal)
  - Refund management
  - Revenue reporting and analytics
  - Tax collection (if applicable)
  - Invoicing and receipts
  - **Affiliate revenue tracking** (Amazon Associates, bookstore programs)
  
- **Customer Management:**
  - Customer profiles and history
  - Email notifications and reminders
  - Review and testimonial collection
  - Loyalty programs
  - Newsletter integration

### Technical Integration
- Mobile app (Blazor Hybrid or React Native)
- Integration with calendar systems (Google Calendar, Outlook)
- Integration with theater booking systems
- CRM integration (Salesforce, HubSpot)
- Accounting software integration (QuickBooks, Xero)

### Enhanced Real-Time Features (using SignalR)
- Notify all users when new videos become available
- Admin dashboard with live statistics
- Multi-user show planning collaboration
- Real-time booking notifications
- Live class enrollment updates

### Social Media Automation
- **Content Distribution:**
  - Auto-post generated videos to social media platforms
  - Supported platforms: YouTube Shorts, TikTok, Instagram Reels, Twitter/X, Facebook, LinkedIn
  - Platform-specific video formatting and optimization
  - Custom captions and descriptions per platform
  - Hashtag management and recommendations
  
- **Scheduling & Publishing:**
  - Content calendar with visual timeline
  - Schedule posts in advance
  - Optimal posting time recommendations (based on engagement analytics)
  - Batch scheduling for multiple platforms
  - Queue management (draft, scheduled, published, failed)
  - Recurring post schedules (e.g., "Warmup Wednesday", "Technique Tuesday")
  
- **Duplicate Prevention:**
  - Track all published posts by platform and content
  - Content fingerprinting (video hash, text similarity)
  - Alert before posting duplicate content
  - Historical post database with search
  - "Last posted" indicators on activities
  - Configurable duplicate rules (e.g., "Don't repost same activity within 30 days")
  
- **Analytics & Tracking:**
  - Track post performance (views, likes, shares, comments)
  - Engagement metrics dashboard
  - Best performing content reports
  - Platform-specific analytics
  - ROI tracking for paid promotions
  - A/B testing for captions and hashtags
  
- **Content Templates:**
  - Pre-defined post templates by activity type
  - Dynamic caption generation using AI
  - Brand voice consistency
  - Hashtag sets by category
  - Call-to-action templates
  
- **Workflow Automation:**
  - Automatically queue new videos for social media
  - Auto-generate captions from activity descriptions
  - Smart hashtag suggestions based on content
  - Cross-posting with platform-specific optimizations
  - Approval workflow for team review before posting

---

## 11. Open Questions

1. Which video generation API provides best results for improv scenes?
2. Should admins be able to customize script parameters (tone, player count)?
3. Should standard users be able to request video generation with admin approval?
4. Video hosting strategy (blob storage, CDN, streaming service)?
5. Monetization model (free, freemium, subscription)?
6. Should there be a super admin role separate from regular admins?
7. How should user registration be handled (open, invite-only, admin approval)?
8. Should standard users be able to suggest video references (with admin approval)?
9. Should we auto-embed videos or link externally to avoid copyright issues?
10. Should we use AI to analyze external videos and auto-generate timestamps?
11. Should activity relationships be bidirectional (both activities show the relationship)?
12. Should we allow standard users to suggest activity relationships?
13. Should search include aliases when looking for activities by name?
14. What additional activity types should we support beyond Game, Technique, Warmup, Exercise?
15. Should activities support multiple types (e.g., a Warmup that's also a Game)?
16. Should we require source attribution for all activities or make it optional?
17. How should we handle activities with multiple sources?
18. Should we verify sources before allowing them to be referenced?
19. Which affiliate programs should we use (Amazon Associates, Bookshop.org, both)?
20. Should affiliate links be clearly disclosed to users?
21. Should we offer users choice between retailers (Amazon vs independent bookstores)?
22. How should affiliate revenue be tracked and reported?
23. Which container deployment platform should we target first (Azure Container Apps, Kubernetes, Docker Compose)?
24. Which social media platforms should we prioritize for automation?
25. Should we allow manual override of duplicate prevention rules?
26. How long should we wait before allowing reposts of the same content?
27. Should we integrate with social media management tools (Buffer, Hootsuite) or build our own?
28. Should we use AI to optimize posting times based on follower analytics?
29. How should we handle social media API rate limits and quotas?
30. Which speech-to-text service should we use (Azure Speech Services, OpenAI Whisper, browser Web Speech API)?
31. Should voice mode be available on mobile, desktop, or both?
32. How should we handle voice transcription errors and allow manual correction?
33. Should voice mode support multiple languages?
34. Should voice mode work offline or require internet connection?
35. What rate limits should apply to anonymous users for show planner? (e.g., 10 plans per IP per hour)
36. Should we require CAPTCHA for anonymous show planner to prevent abuse?
37. Should anonymous users be able to export/print show plans, or require login for that?
38. Should we track anonymous usage analytics (by IP, aggregated)?

---

## 12. Appendix

### Technology Decisions Rationale

**Why .NET over Node.js/Python:**
- Type safety across entire stack
- Better tooling and debugging experience
- Semantic Kernel for AI orchestration
- Aspire for modern cloud-native development
- Single language (C#) for full stack

**Why Blazor over React/Vue:**
- Consistency with .NET backend
- Share models between frontend/backend
- Auto render mode for optimal performance
- Strong typing in UI layer
- Smaller team, one language to maintain

**Why PostgreSQL over MongoDB:**
- Structured data model (games, scripts, videos)
- Strong relational integrity
- Better for search/filter operations
- Mature EF Core integration

**Why BackgroundService over Celery:**
- Simpler architecture (no Python, Redis, separate workers)
- Easier development and debugging
- Sufficient for moderate scale
- Can scale to separate worker project if needed

**Identity DbContext Configuration:**
```csharp
public class ImprovDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<ImprovActivity> Activities { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }
    public DbSet<ActivitySource> ActivitySources { get; set; }
    public DbSet<Difficulty> Difficulties { get; set; }
    // ... other DbSets
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Required for Identity
        
        // Configure relationships with proper delete behavior
        builder.Entity<ImprovActivity>()
            .HasOne(a => a.CreatedBy)
            .WithMany()
            .HasForeignKey(a => a.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<ActivitySource>()
            .HasOne(s => s.CreatedBy)
            .WithMany()
            .HasForeignKey(s => s.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
        
        // ... other entity configurations
    }
}
```

**Identity Configuration in Program.cs:**
```csharp
builder.Services.AddDbContext<ImprovDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password requirements
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    
    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ImprovDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor(); // For accessing current user
```
