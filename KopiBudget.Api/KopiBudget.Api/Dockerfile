# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the csproj first (to leverage caching)
COPY ["KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/"]

RUN dotnet restore "KopiBudget.Api/KopiBudget.Api.csproj"

# Copy the rest of the code
COPY . .
WORKDIR "/src/KopiBudget.Api"

# Publish the app
RUN dotnet publish "KopiBudget.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
