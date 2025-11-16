# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY Services/OrderService/OrderService.csproj ./Services/OrderService/
RUN dotnet restore Services/OrderService/OrderService.csproj

# Copy the rest of the project and build
COPY Services/OrderService/. ./Services/OrderService/
WORKDIR /app/Services/OrderService
RUN dotnet build -c Release

# Publish the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Services/OrderService/out .

# Expose port
EXPOSE 5003

# Run the application
ENTRYPOINT ["dotnet", "OrderService.dll"]