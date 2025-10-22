# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY CvAnalyzer.sln ./
COPY CvAnalyzer.Api/CvAnalyzer.Api.csproj ./CvAnalyzer.Api/
COPY CvAnalyzer.Core/CvAnalyzer.Core.csproj ./CvAnalyzer.Core/
COPY CvAnalyzer.Infrastructure/CvAnalyzer.Infrastructure.csproj ./CvAnalyzer.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY . ./

# Build and publish the application
RUN dotnet publish CvAnalyzer.Api/CvAnalyzer.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5161
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the port
EXPOSE 5161

# Run the application
ENTRYPOINT ["dotnet", "CvAnalyzer.Api.dll"]
