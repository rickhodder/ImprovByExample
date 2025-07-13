#!/bin/bash

# Setup Development Environment for ImprovByExample

echo "🎭 Setting up ImprovByExample Development Environment"
echo "=================================================="

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker is not running. Please start Docker Desktop and try again."
    exit 1
fi

echo "✅ Docker is running"

# Navigate to project directory
cd "$(dirname "$0")/.."

# Pull latest images
echo "📦 Pulling latest Docker images..."
docker-compose pull

# Start services
echo "🚀 Starting development services..."
docker-compose up -d postgres redis

# Wait for PostgreSQL to be ready
echo "⏳ Waiting for PostgreSQL to be ready..."
until docker-compose exec postgres pg_isready -U postgres > /dev/null 2>&1; do
    sleep 1
done

echo "✅ PostgreSQL is ready!"

# Show running containers
echo "📋 Running containers:"
docker-compose ps

echo ""
echo "🎉 Development environment is ready!"
echo ""
echo "📊 Services:"
echo "  - PostgreSQL: localhost:5432"
echo "  - Redis: localhost:6379"
echo "  - pgAdmin: http://localhost:5050 (admin@improvbyexample.com / admin)"
echo ""
echo "🔧 Next steps:"
echo "  1. Install EF Core tools: dotnet tool install --global dotnet-ef"
echo "  2. Add PostgreSQL package: dotnet add src/ImprovByExample.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL"
echo "  3. Create first migration: dotnet ef migrations add InitialCreate --project src/ImprovByExample.Infrastructure"
echo "  4. Update database: dotnet ef database update --project src/ImprovByExample.Infrastructure"
echo ""
echo "🐳 To view containers in VS Code:"
echo "  - Open Docker panel (Ctrl+Shift+P → 'Docker: Focus on Containers View')"
echo "  - Right-click containers for logs, shell access, etc."
