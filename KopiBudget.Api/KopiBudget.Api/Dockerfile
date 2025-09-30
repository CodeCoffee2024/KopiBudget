FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy from current context (KopiBudget.Api directory)
COPY ["KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/"]
COPY ["KopiBudget.Application/KopiBudget.Application.csproj", "../KopiBudget.Application/"]
COPY ["KopiBudget.Infrastructure/KopiBudget.Infrastructure.csproj", "../KopiBudget.Infrastructure/"]
COPY ["KopiBudget.Domain/KopiBudget.Domain.csproj", "../KopiBudget.Domain/"]
COPY ["KopiBudget.Common/KopiBudget.Common.csproj", "../KopiBudget.Common/"]

RUN dotnet restore "./KopiBudget.Api/KopiBudget.Api.csproj"

# Copy everything from the root context
COPY .. .

# Remove test projects
RUN rm -rf ../KopiBudget.Tests/

WORKDIR "/src/KopiBudget.Api"
RUN dotnet build "./KopiBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KopiBudget.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
