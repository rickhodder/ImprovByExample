FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/ImprovByExample.Api/ImprovByExample.Api.csproj", "src/ImprovByExample.Api/"]
COPY ["src/ImprovByExample.Application/ImprovByExample.Application.csproj", "src/ImprovByExample.Application/"]
COPY ["src/ImprovByExample.Domain/ImprovByExample.Domain.csproj", "src/ImprovByExample.Domain/"]
COPY ["src/ImprovByExample.Infrastructure/ImprovByExample.Infrastructure.csproj", "src/ImprovByExample.Infrastructure/"]

RUN dotnet restore "src/ImprovByExample.Api/ImprovByExample.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/src/ImprovByExample.Api"
RUN dotnet build "ImprovByExample.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ImprovByExample.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImprovByExample.Api.dll"]