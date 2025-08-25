FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore
COPY ["KopiBudget.Api/KopiBudget.Api/KopiBudget.Api.csproj", "KopiBudget.Api/"]
RUN dotnet restore "KopiBudget.Api/KopiBudget.Api/KopiBudget.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/KopiBudget.Api"
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KopiBudget.Api.dll"]
