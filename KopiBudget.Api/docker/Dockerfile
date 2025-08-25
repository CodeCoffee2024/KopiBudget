# Base runtime image (small & optimized)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore dependencies (cacheable)
COPY ["KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/"]
RUN dotnet restore "KopiBudget.Api/KopiBudget.Api.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/KopiBudget.Api"

# Build the project
RUN dotnet build "KopiBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KopiBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
