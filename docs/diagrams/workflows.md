# Workflow Diagrams

## Authentication Flow

This sequence diagram shows the complete authentication process from login to session establishment.

```mermaid
sequenceDiagram
    actor User
    participant Web as Blazor Web App
    participant API as ASP.NET Core API
    participant Auth as AuthController
    participant Identity as UserManager & SignInManager
    participant DB as PostgreSQL Database
    
    User->>Web: Enter email & password
    Web->>API: POST /api/auth/login<br/>{email, password}
    API->>Auth: LoginAsync(LoginDto)
    Auth->>Identity: FindByEmailAsync(email)
    Identity->>DB: Query ApplicationUser
    
    alt User Not Found
        DB-->>Identity: null
        Identity-->>Auth: User not found
        Auth-->>API: 401 Unauthorized
        API-->>Web: Error response
        Web-->>User: "Invalid credentials"
    else User Found
        DB-->>Identity: ApplicationUser
        Identity->>Identity: CheckPasswordAsync(user, password)
        
        alt Password Invalid
            Identity-->>Auth: Sign-in failed
            Auth-->>API: 401 Unauthorized
            API-->>Web: Error response
            Web-->>User: "Invalid credentials"
        else Password Valid
            Identity->>Identity: SignInAsync(user)
            Identity->>DB: Create session
            DB-->>Identity: Session created
            Identity-->>Auth: Success
            Auth-->>API: Set authentication cookie
            API-->>Web: 200 OK + auth cookie
            Web->>Web: Update UI state
            Web-->>User: Redirect to dashboard/home
        end
    end
```

**Key Points:**
- Cookie-based authentication (not JWT)
- Passwords hashed with ASP.NET Identity defaults
- Email used as username
- Failed attempts don't reveal whether email exists
- Session persisted server-side in database

---

## Video Generation Workflow

This sequence diagram illustrates the asynchronous video generation process with real-time progress updates via SignalR.

```mermaid
sequenceDiagram
    actor Admin
    participant UI as Blazor UI
    participant API as Activities API
    participant BG as Background Service
    participant Hub as VideoProgressHub<br/>(SignalR)
    participant VideoAPI as Video Generation API<br/>(External)
    participant DB as PostgreSQL Database
    
    Admin->>UI: Click "Generate Video"<br/>for activity
    UI->>Hub: Connect to SignalR<br/>Subscribe to activity updates
    Hub-->>UI: Connection established
    
    UI->>API: POST /api/activities/{id}/generate-video
    API->>DB: INSERT VideoGenerationRequest<br/>Status: Pending
    DB-->>API: Request created (ID: 123)
    API->>BG: Queue generation job<br/>(Activity ID, Request ID)
    API-->>UI: 202 Accepted<br/>{requestId: 123}
    UI-->>Admin: Show progress spinner<br/>"Generating video..."
    
    Note over BG: Background processing starts
    
    BG->>DB: UPDATE Status = 'Processing'
    BG->>Hub: Broadcast progress<br/>{activityId, status: 'Processing', progress: 0}
    Hub-->>UI: Receive update
    UI-->>Admin: Update progress bar: 0%
    
    BG->>VideoAPI: POST /generate<br/>{script, activity details}
    
    loop Video Generation (polling or webhooks)
        VideoAPI-->>BG: Progress update: 25%
        BG->>Hub: Broadcast progress<br/>{activityId, progress: 25}
        Hub-->>UI: Receive update
        UI-->>Admin: Update progress bar: 25%
        
        VideoAPI-->>BG: Progress update: 50%
        BG->>Hub: Broadcast progress<br/>{activityId, progress: 50}
        Hub-->>UI: Receive update
        UI-->>Admin: Update progress bar: 50%
        
        VideoAPI-->>BG: Progress update: 75%
        BG->>Hub: Broadcast progress<br/>{activityId, progress: 75}
        Hub-->>UI: Receive update
        UI-->>Admin: Update progress bar: 75%
    end
    
    VideoAPI-->>BG: Video complete<br/>{videoUrl, thumbnailUrl}
    BG->>DB: UPDATE VideoGenerationRequest<br/>Status: 'Completed'<br/>VideoUrl: 'https://...'
    BG->>Hub: Broadcast completion<br/>{activityId, status: 'Completed', videoUrl}
    Hub-->>UI: Receive completion
    UI->>UI: Load video player
    UI-->>Admin: Display video<br/>Show success message
    
    alt Video Generation Fails
        VideoAPI-->>BG: Error response
        BG->>DB: UPDATE Status = 'Failed'<br/>ErrorMessage: '...'
        BG->>Hub: Broadcast error<br/>{activityId, status: 'Failed', error}
        Hub-->>UI: Receive error
        UI-->>Admin: Show error message<br/>"Video generation failed"
    end
```

**Implementation Notes:**
- SignalR enables real-time updates without polling
- Background service decouples generation from API request
- Progress percentage estimated based on generation stages
- Failures logged with detailed error messages
- Admin can navigate away and return later to check status

---

## Show Planner Algorithm

This flowchart shows the AI-assisted process for creating balanced show agendas.

```mermaid
flowchart TD
    Start([Admin Opens Show Planner])
    InputPlayers[Enter Player Names<br/>e.g., Alice, Bob, Charlie, Dana]
    SelectActivities[Select Activities<br/>from Database]
    InputConstraints[Set Constraints<br/>• Show duration<br/>• Activity preferences<br/>• Player availability]
    
    AIProcess{AI Optimization}
    
    BalancePlayers[Balance Player<br/>Distribution<br/>Ensure fair stage time]
    OrderActivities[Order Activities<br/>• Start with warmup<br/>• Build energy<br/>• End strong]
    CheckDuration{Total Duration<br/>Within Limit?}
    AssignPlayers[Assign Players<br/>to Each Activity]
    
    Preview[Preview Show Agenda<br/>• Activity order<br/>• Player assignments<br/>• Estimated times]
    
    AdminReview{Admin Review}
    ManualAdjust[Manual Adjustments<br/>• Reorder activities<br/>• Reassign players<br/>• Add/remove activities]
    
    SaveShow[Save Show to Database]
    GenerateCards[Generate Show Cards<br/>• Print-friendly format<br/>• One card per activity<br/>• Player assignments visible]
    
    End([Show Ready for Performance])
    
    Start --> InputPlayers
    InputPlayers --> SelectActivities
    SelectActivities --> InputConstraints
    InputConstraints --> AIProcess
    
    AIProcess --> BalancePlayers
    BalancePlayers --> OrderActivities
    OrderActivities --> CheckDuration
    
    CheckDuration -->|No| ManualAdjust
    CheckDuration -->|Yes| AssignPlayers
    AssignPlayers --> Preview
    
    Preview --> AdminReview
    AdminReview -->|Needs Changes| ManualAdjust
    ManualAdjust --> Preview
    
    AdminReview -->|Approved| SaveShow
    SaveShow --> GenerateCards
    GenerateCards --> End
    
    style Start fill:#90EE90
    style AIProcess fill:#FFD700
    style AdminReview fill:#87CEEB
    style End fill:#90EE90
```

**Algorithm Considerations:**
- **Player Balance**: Each player gets approximately equal stage time
- **Activity Ordering**: 
  - Start: Warmup/easy activities to get audience comfortable
  - Middle: Core games with medium-high energy
  - End: Strong closer or signature game
- **Duration Management**: Sum of estimated activity durations ≤ show duration
- **Player Availability**: Respect player constraints (e.g., not available for certain activities)
- **Variety**: Mix activity types (games, scenes, musical) for audience engagement

**Future Enhancements:**
- Learn from past successful show structures
- Recommend activities based on player strengths
- Consider audience demographics
- Suggest substitutes if key players unavailable

---

## Activity Creation Workflow

This diagram shows the process for adding new activities with voice input option.

```mermaid
flowchart TD
    Start([Admin: Create Activity])
    InputMode{Input Mode?}
    
    ManualEntry[Type Activity Details<br/>• Name<br/>• Description<br/>• Rules]
    VoiceEntry[Voice Input Mode<br/>🎤 Dictate details]
    
    Transcribe[Speech-to-Text<br/>Conversion]
    Review[Review Transcription<br/>Make corrections]
    
    SetMetadata[Set Metadata<br/>• Type<br/>• Difficulty<br/>• Players<br/>• Duration]
    
    AddSource[Add Source Attribution<br/>Optional]
    AddScript[Add Example Script<br/>Optional]
    AddVideoRefs[Add External Video Links<br/>Optional]
    AddRelationships[Link Related Activities<br/>Optional]
    
    Validate{Validation}
    ValidationErrors[Show Errors<br/>• Name required<br/>• Min players ≤ max<br/>• Duration > 0]
    
    Save[Save to Database]
    Success[Activity Created<br/>Show success message]
    
    GenerateVideo{Generate<br/>AI Video?}
    QueueVideo[Queue Video Generation<br/>See Video Workflow]
    
    End([Return to Activity List])
    
    Start --> InputMode
    InputMode -->|Manual| ManualEntry
    InputMode -->|Voice| VoiceEntry
    
    VoiceEntry --> Transcribe
    Transcribe --> Review
    Review --> SetMetadata
    ManualEntry --> SetMetadata
    
    SetMetadata --> AddSource
    AddSource --> AddScript
    AddScript --> AddVideoRefs
    AddVideoRefs --> AddRelationships
    AddRelationships --> Validate
    
    Validate -->|Errors| ValidationErrors
    ValidationErrors --> SetMetadata
    
    Validate -->|Valid| Save
    Save --> Success
    Success --> GenerateVideo
    
    GenerateVideo -->|Yes| QueueVideo
    GenerateVideo -->|No| End
    QueueVideo --> End
    
    style Start fill:#90EE90
    style InputMode fill:#FFD700
    style Validate fill:#FFD700
    style GenerateVideo fill:#FFD700
    style End fill:#90EE90
```

**Voice Input Benefits:**
- Faster data entry for multiple activities
- Hands-free operation during activity research
- Natural language description capture
- Reduces typing fatigue for large imports

**Validation Rules:**
- Name: Required, max 200 chars
- Min/Max Players: Min ≤ Max, both > 0
- Duration: > 0 minutes
- Activity Type: Required (from seeded types)
- Difficulty: Required (from seeded difficulties)
