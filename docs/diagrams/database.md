# Database Diagrams

## Entity Relationship Diagram (ERD)

This diagram shows the complete database schema with all entities and their relationships.

```mermaid
erDiagram
    ApplicationUser ||--o{ ImprovActivity : "creates"
    ApplicationUser ||--o{ ActivitySource : "creates"
    ApplicationUser ||--o{ Show : "creates"
    
    ActivityType ||--o{ ImprovActivity : "categorizes"
    ActivitySource ||--o{ ImprovActivity : "attributes"
    Difficulty ||--o{ ImprovActivity : "rates"
    SourceType ||--o{ ActivitySource : "classifies"
    VideoPlatform ||--o{ ExternalVideoReference : "hosts"
    
    ImprovActivity ||--o{ VideoGenerationRequest : "generates"
    ImprovActivity ||--o{ ExternalVideoReference : "references"
    ImprovActivity ||--o{ ShowActivity : "included in"
    
    ExternalVideoReference ||--o{ VideoTimestamp : "contains"
    
    ImprovActivity ||--o{ ActivityRelationship : "relates from"
    ImprovActivity ||--o{ ActivityRelationship : "relates to"
    RelationshipType ||--o{ ActivityRelationship : "defines"
    
    Show ||--o{ ShowActivity : "contains"
    
    ApplicationUser {
        string Id PK
        string UserName
        string Email
        string FirstName
        string LastName
        datetime CreatedAt
        bool EmailConfirmed
    }
    
    ImprovActivity {
        int Id PK
        string Name
        string Description
        string Rules
        string ExampleScript
        int MinPlayers
        int MaxPlayers
        int DurationMinutes
        int ActivityTypeId FK
        int DifficultyId FK
        int ActivitySourceId FK "nullable"
        string CreatedBy FK
        datetime CreatedAt
        string LastModifiedBy FK
        datetime LastModifiedAt
    }
    
    ActivityType {
        int Id PK
        string Name
        string Description
        string CreatedBy FK
        datetime CreatedAt
    }
    
    Difficulty {
        int Id PK
        string Name
        int Level
        string CreatedBy FK
        datetime CreatedAt
    }
    
    ActivitySource {
        int Id PK
        string Title
        string Author
        int SourceTypeId FK
        string Url
        string AffiliateLink
        int PublicationYear "nullable"
        string CreatedBy FK
        datetime CreatedAt
    }
    
    SourceType {
        int Id PK
        string Name
        string CreatedBy FK
        datetime CreatedAt
    }
    
    VideoGenerationRequest {
        int Id PK
        int ActivityId FK
        string Status
        string VideoUrl
        string ErrorMessage
        datetime RequestedAt
        datetime CompletedAt
    }
    
    ExternalVideoReference {
        int Id PK
        int ActivityId FK
        string Title
        string Url
        int VideoPlatformId FK "nullable"
        string Description
        string CreatedBy FK
        datetime CreatedAt
    }
    
    VideoPlatform {
        int Id PK
        string Name
        string UrlPattern
        string CreatedBy FK
        datetime CreatedAt
    }
    
    VideoTimestamp {
        int Id PK
        int ExternalVideoReferenceId FK
        int TimestampSeconds
        string Description
        string CreatedBy FK
        datetime CreatedAt
    }
    
    ActivityRelationship {
        int Id PK
        int ActivityId FK
        int RelatedActivityId FK
        int RelationshipTypeId FK
        string Notes
        string CreatedBy FK
        datetime CreatedAt
    }
    
    RelationshipType {
        int Id PK
        string Name
        string Description
        string CreatedBy FK
        datetime CreatedAt
    }
    
    Show {
        int Id PK
        string Name
        string Description
        datetime ShowDate
        string CreatedBy FK
        datetime CreatedAt
        string LastModifiedBy FK
        datetime LastModifiedAt
    }
    
    ShowActivity {
        int Id PK
        int ShowId FK
        int ActivityId FK
        int OrderIndex
        string AssignedPlayers
        string Notes
        int EstimatedDurationMinutes
    }
```

---

## Core Entity Relationships Detail

### Activity Ecosystem
The central entity is `ImprovActivity`, which connects to:
- **ActivityType** - Categorization (game, warmup, exercise, technique)
- **Difficulty** - Skill level rating
- **ActivitySource** - Attribution to books, workshops, or websites
- **VideoGenerationRequest** - AI-generated demonstration videos
- **ExternalVideoReference** - Links to external video examples

### Activity Relationships
Activities can relate to each other through `ActivityRelationship`:
- **Alias** - Same activity, different name
- **Variation** - Modified version of an activity
- **Similar** - Related activities with comparable mechanics

### Show Planning
The `Show` and `ShowActivity` entities enable:
- Creating performance agendas
- Ordering activities sequentially
- Assigning players to each activity
- Tracking estimated durations

### User Management
`ApplicationUser` tracks:
- Authentication via ASP.NET Identity
- Content creation/modification attribution
- Future: User preferences, favorites, history

---

## Table Relationships Summary

| Parent Entity | Child Entity | Relationship | Description |
|--------------|--------------|--------------|-------------|
| ApplicationUser | ImprovActivity | One-to-Many | User creates activities |
| ApplicationUser | ActivitySource | One-to-Many | User adds sources |
| ApplicationUser | Show | One-to-Many | User creates shows |
| ActivityType | ImprovActivity | One-to-Many | Type categorizes activities |
| Difficulty | ImprovActivity | One-to-Many | Difficulty rates activities |
| ActivitySource | ImprovActivity | One-to-Many | Source attributes activities |
| ImprovActivity | VideoGenerationRequest | One-to-Many | Activity has video generations |
| ImprovActivity | ExternalVideoReference | One-to-Many | Activity references external videos |
| ImprovActivity | ActivityRelationship | Many-to-Many | Activities relate to each other |
| ImprovActivity | ShowActivity | One-to-Many | Activity included in shows |
| ExternalVideoReference | VideoTimestamp | One-to-Many | Video has multiple timestamps |
| Show | ShowActivity | One-to-Many | Show contains ordered activities |
| RelationshipType | ActivityRelationship | One-to-Many | Type defines relationships |
| VideoPlatform | ExternalVideoReference | One-to-Many | Platform hosts videos |
| SourceType | ActivitySource | One-to-Many | Type classifies sources |
