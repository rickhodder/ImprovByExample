# Code Generation Prompts

## Entity Creation
```
I need to create a domain entity for [Entity Name] with the following properties:
- [List properties with types and constraints]

Please show me:
- The entity class following DDD principles
- Any value objects that should be extracted
- Validation rules
- How this fits in Clean Architecture
```

## API Endpoint Generation
```
I need to create a Fast Endpoint for [Operation] with:
- Input: [describe input]
- Output: [describe output]
- Business rules: [list rules]

Please provide:
- The endpoint class
- Request/Response DTOs
- Validation rules
- Integration with the application layer
```

## Database Configuration
```
I need EF Core configuration for [Entity Name] including:
- Table mapping
- Relationships to [Other Entities]
- Constraints and indexes
- Any special configurations

Please show both Fluent API and Data Annotations approaches.
```

## Service Implementation
```
I need to implement [Service Name] that:
- [List responsibilities]
- Uses [Dependencies]
- Follows [Patterns]

Please provide:
- Interface definition
- Implementation with dependency injection
- Error handling
- Unit test structure
```

## Specification Pattern
```
I need to implement specifications for [Entity] to handle:
- [List filtering criteria]
- [List business rules]

Please show:
- Specification classes
- How to compose them
- Integration with repositories
- Usage examples
```
