# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["ImprovByExample.sln", "./"]
COPY ["src/ImprovByExample.Api/ImprovByExample.Api.csproj", "src/ImprovByExample.Api/"]
COPY ["src/ImprovByExample.Application/ImprovByExample.Application.csproj", "src/ImprovByExample.Application/"]
COPY ["src/ImprovByExample.Domain/ImprovByExample.Domain.csproj", "src/ImprovByExample.Domain/"]
COPY ["src/ImprovByExample.Infrastructure/ImprovByExample.Infrastructure.csproj", "src/ImprovByExample.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "src/ImprovByExample.Api/ImprovByExample.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/src/ImprovByExample.Api"
RUN dotnet build "ImprovByExample.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ImprovByExample.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Create logs directory
RUN mkdir -p /app/logs

# Copy published files
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "ImprovByExample.Api.dll"]
