# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY Services/PaymentService/PaymentService.csproj ./Services/PaymentService/
RUN dotnet restore Services/PaymentService/PaymentService.csproj

# Copy the rest of the project and build
COPY Services/PaymentService/. ./Services/PaymentService/
WORKDIR /app/Services/PaymentService
RUN dotnet build -c Release

# Publish the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Services/PaymentService/out .

# Expose port
EXPOSE 5004

# Run the application
ENTRYPOINT ["dotnet", "PaymentService.dll"]