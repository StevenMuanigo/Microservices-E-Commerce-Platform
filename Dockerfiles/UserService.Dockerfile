# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY Services/UserService/UserService.csproj ./Services/UserService/
RUN dotnet restore Services/UserService/UserService.csproj

# Copy the rest of the project and build
COPY Services/UserService/. ./Services/UserService/
WORKDIR /app/Services/UserService
RUN dotnet build -c Release

# Publish the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Services/UserService/out .

# Expose port
EXPOSE 5002

# Run the application
ENTRYPOINT ["dotnet", "UserService.dll"]