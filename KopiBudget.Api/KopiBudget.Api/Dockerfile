FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all csproj files (API + dependencies + tests)
COPY ["KopiBudget.Api/KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/"]
COPY ["KopiBudget.Application/KopiBudget.Application.csproj", "KopiBudget.Application/"]
COPY ["KopiBudget.Infrastructure/KopiBudget.Infrastructure.csproj", "KopiBudget.Infrastructure/"]
COPY ["KopiBudget.Common/KopiBudget.Common.csproj", "KopiBudget.Common/"]
COPY ["KopiBudget.Domain/KopiBudget.Domain.csproj", "KopiBudget.Domain/"]
COPY ["KopiBudget.Tests/KopiBudget.Tests.csproj", "KopiBudget.Tests/"]

# Restore dependencies at solution level
RUN dotnet restore "KopiBudget.Api/KopiBudget.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build API
WORKDIR "/src/KopiBudget.Api"
RUN dotnet build "KopiBudget.Api.csproj" -c Release -o /app/build

# Run tests
WORKDIR "/src/KopiBudget.Tests"
RUN dotnet test "KopiBudget.Tests.csproj" -c Release --no-build --verbosity normal

# Publish API
WORKDIR "/src/KopiBudget.Api"
RUN dotnet publish "KopiBudget.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
