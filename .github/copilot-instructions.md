# GitHub Copilot Instructions

## Git Operations
- **Never execute git commands** (add, commit, push, pull, merge, checkout, etc.)
- Let the user handle all git operations manually
- You may read git status or diffs when needed, but don't modify git state
- If changes need to be committed, inform the user but don't execute the commands

## General Guidelines
- Follow clean architecture principles
- Use established patterns in the codebase
- Write tests for new functionality
- Keep responses concise and focused
