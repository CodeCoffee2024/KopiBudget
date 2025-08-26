FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Add this before "RUN dotnet build"
COPY ["KopiBudget.Api/KopiBudget.Tests/KopiBudget.Tests.csproj", "KopiBudget.Tests/"]
RUN dotnet restore "./KopiBudget.Tests/KopiBudget.Tests.csproj"

# Copy rest of the files
COPY . .

# Run tests (fail build if tests fail)
WORKDIR "/src/KopiBudget.Tests"
RUN dotnet test --no-restore --no-build -c $BUILD_CONFIGURATION

# Copy project file and restore
COPY ["KopiBudget.Api/KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/"]
RUN dotnet restore "KopiBudget.Api/KopiBudget.Api.csproj"

# Copy everything else and build
COPY KopiBudget.Api/. ./KopiBudget.Api/
WORKDIR "/src/KopiBudget.Api"

RUN dotnet publish "KopiBudget.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
