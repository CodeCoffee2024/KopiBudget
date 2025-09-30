# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy ONLY the projects that actually exist
COPY ["../KopiBudget.Api/KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/"]
# Remove or comment out these lines if they don't exist:
COPY ["../KopiBudget.Application/KopiBudget.Application.csproj", "KopiBudget.Application/"]
COPY ["../KopiBudget.Infrastructure/KopiBudget.Infrastructure.csproj", "KopiBudget.Infrastructure/"]
COPY ["../KopiBudget.Domain/KopiBudget.Domain.csproj", "KopiBudget.Domain/"]
COPY ["../KopiBudget.Common/KopiBudget.Common.csproj", "KopiBudget.Common/"]

RUN dotnet restore "./KopiBudget.Api/KopiBudget.Api.csproj"

# Copy source code
COPY . .

# Remove test projects to avoid build issues
RUN rm -rf KopiBudget.Tests/

WORKDIR "/src/KopiBudget.Api"
RUN dotnet build "./KopiBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KopiBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
