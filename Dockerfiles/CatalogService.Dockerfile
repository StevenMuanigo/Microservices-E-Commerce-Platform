# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY Services/CatalogService/CatalogService.csproj ./Services/CatalogService/
RUN dotnet restore Services/CatalogService/CatalogService.csproj

# Copy the rest of the project and build
COPY Services/CatalogService/. ./Services/CatalogService/
WORKDIR /app/Services/CatalogService
RUN dotnet build -c Release

# Publish the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Services/CatalogService/out .

# Expose port
EXPOSE 5001

# Run the application
ENTRYPOINT ["dotnet", "CatalogService.dll"]