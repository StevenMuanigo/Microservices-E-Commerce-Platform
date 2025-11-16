# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY Services/CartService/CartService.csproj ./Services/CartService/
RUN dotnet restore Services/CartService/CartService.csproj

# Copy the rest of the project and build
COPY Services/CartService/. ./Services/CartService/
WORKDIR /app/Services/CartService
RUN dotnet build -c Release

# Publish the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Services/CartService/out .

# Expose port
EXPOSE 5005

# Run the application
ENTRYPOINT ["dotnet", "CartService.dll"]