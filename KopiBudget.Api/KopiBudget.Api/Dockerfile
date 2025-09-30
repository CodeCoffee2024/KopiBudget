FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY *.sln ./

# Copy csproj files (note the nested structure)
COPY ["KopiBudget.Api/KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/KopiBudget.Api/"]
COPY ["KopiBudget.Application/KopiBudget.Application.csproj", "KopiBudget.Application/"]
COPY ["KopiBudget.Infrastructure/KopiBudget.Infrastructure.csproj", "KopiBudget.Infrastructure/"]
COPY ["KopiBudget.Domain/KopiBudget.Domain.csproj", "KopiBudget.Domain/"]
COPY ["KopiBudget.Common/KopiBudget.Common.csproj", "KopiBudget.Common/"]

# Restore dependencies
RUN dotnet restore "KopiBudget.Api/KopiBudget.Api/KopiBudget.Api.csproj"

# Copy the rest of the source code
COPY . .

WORKDIR "/src/KopiBudget.Api/KopiBudget.Api"

# Build and publish
RUN dotnet publish "KopiBudget.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
