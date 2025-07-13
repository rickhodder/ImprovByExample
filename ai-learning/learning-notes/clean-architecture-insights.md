# Clean Architecture Learning Notes

## Key Concepts Learned

### Layer Responsibilities
- **Domain Layer**: Pure business logic, entities, value objects
- **Application Layer**: Use cases, specifications, orchestration
- **Infrastructure Layer**: External concerns, database, caching
- **Presentation Layer**: UI, API endpoints, controllers

### Dependency Flow
- Dependencies point inward toward the domain
- Outer layers depend on inner layers
- Inner layers never depend on outer layers
- Use dependency injection to maintain this flow

### Feature Organization
- Organize by features rather than technical concerns
- Each feature contains its own commands, queries, and handlers
- Shared components go in Common folders
- Domain entities can be shared across features

## Practical Applications

### Project Structure
```
Application/
├── Common/
│   ├── Interfaces/
│   └── Specifications/
└── Features/
    └── Games/
        ├── Commands/
        ├── Queries/
        └── Handlers/
```

### Command/Query Separation
- Commands: Change state, return void or simple result
- Queries: Read data, never change state
- Use MediatR for handling both patterns

### Specification Pattern
- Encapsulate business rules in reusable specifications
- Compose specifications for complex queries
- Keep business logic out of repositories

## Common Pitfalls to Avoid
1. **Dependency Violations**: Never reference outer layers from inner layers
2. **Anemic Domain Models**: Ensure entities have behavior, not just data
3. **Fat Controllers**: Keep presentation layer thin
4. **Tight Coupling**: Use interfaces for dependencies

## Next Learning Goals
- [ ] Implement domain events
- [ ] Master specification composition
- [ ] Understand aggregate design
- [ ] Learn event sourcing basics
