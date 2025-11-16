# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY ApiGateway/ApiGateway.csproj ./ApiGateway/
RUN dotnet restore ApiGateway/ApiGateway.csproj

# Copy the rest of the project and build
COPY ApiGateway/. ./ApiGateway/
WORKDIR /app/ApiGateway
RUN dotnet build -c Release

# Publish the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/ApiGateway/out .

# Expose port
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "ApiGateway.dll"]