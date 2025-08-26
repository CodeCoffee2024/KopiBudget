# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files first (for better caching)
COPY ["KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/"]
COPY ["KopiBudget.Tests/KopiBudget.Tests.csproj", "KopiBudget.Tests/"]

# Restore dependencies
RUN dotnet restore "KopiBudget.Api/KopiBudget.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build API
WORKDIR "/src/KopiBudget.Api"
RUN dotnet build "KopiBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Run tests (fail the build if they fail)
WORKDIR /src/KopiBudget.Tests
RUN dotnet test --no-build -c $BUILD_CONFIGURATION

# Publish API
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/KopiBudget.Api"
RUN dotnet publish "KopiBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
