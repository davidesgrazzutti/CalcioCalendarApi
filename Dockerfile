# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy csproj and restore
COPY *.csproj .
RUN dotnet restore

# Copy all source
COPY . .

# Publish app
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app .

# Expose port
EXPOSE 8080

# Start
ENTRYPOINT ["dotnet", "CalcioCalendarApi.dll"]
