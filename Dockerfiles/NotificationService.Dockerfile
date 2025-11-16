# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY Services/NotificationService/NotificationService.csproj ./Services/NotificationService/
RUN dotnet restore Services/NotificationService/NotificationService.csproj

# Copy the rest of the project and build
COPY Services/NotificationService/. ./Services/NotificationService/
WORKDIR /app/Services/NotificationService
RUN dotnet build -c Release

# Publish the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Services/NotificationService/out .

# Expose port
EXPOSE 5006

# Run the application
ENTRYPOINT ["dotnet", "NotificationService.dll"]