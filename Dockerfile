# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY ["KopiBudget.Api/KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/"]
COPY ["KopiBudget.Tests/KopiBudget.Tests.csproj", "KopiBudget.Tests/"]

RUN dotnet restore "KopiBudget.Api/KopiBudget.Api.csproj"

# Copy source code
COPY . .

# Build API
WORKDIR "/src/KopiBudget.Api"
RUN dotnet build "KopiBudget.Api.csproj" -c Release -o /app/build

# Run tests
WORKDIR /src/KopiBudget.Tests
RUN dotnet test --no-build -c Release

# Publish API
WORKDIR /src/KopiBudget.Api
RUN dotnet publish "KopiBudget.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
