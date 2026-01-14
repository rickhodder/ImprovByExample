# ImprovByExample - System Diagrams

This directory contains visual documentation of the ImprovByExample system architecture, data models, workflows, and deployment processes using Mermaid diagrams.

## Contents

### [Architecture Diagrams](architecture.md)
- **Clean Architecture Layers** - Visual representation of the layered architecture (Domain, Application, Infrastructure, Presentation)
- **User Access Levels** - Diagram showing capabilities for Anonymous, Authenticated, and Admin users
- **Repository Pattern with Specifications** - Class diagram showing the repository pattern implementation

### [Database Diagrams](database.md)
- **Entity Relationship Diagram (ERD)** - Complete database schema showing all entities and their relationships

### [Workflow Diagrams](workflows.md)
- **Authentication Flow** - Sequence diagram for user login process
- **Video Generation Workflow** - End-to-end process for AI video generation with SignalR updates
- **Show Planner Algorithm** - Flowchart of the AI-assisted show planning process

### [Deployment Diagrams](deployment.md)
- **CI/CD Pipeline** - Continuous integration and deployment workflow
- **Container Architecture** - Docker container structure and relationships

## Viewing Diagrams

These diagrams use [Mermaid](https://mermaid.js.org/) syntax and can be viewed:
- In GitHub (native Mermaid support in markdown)
- In VS Code with the [Mermaid Preview](https://marketplace.visualstudio.com/items?itemName=vstirbu.vscode-mermaid-preview) extension
- On any markdown viewer that supports Mermaid

## Updating Diagrams

When updating diagrams:
1. Follow the existing Mermaid syntax conventions
2. Keep diagrams focused and not overly complex
3. Update this README if adding new diagram categories
4. Reference diagrams in the main [PRD](../ImprovByExample-PRD.md) where relevant
