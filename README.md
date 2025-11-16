# Microservices-E-Commerce-Platform

# Microservices E-Commerce Platform

This project is an e-commerce platform developed using modern microservices architecture. The platform consists of six main microservices:

## Microservices

1. **CatalogService** - Product catalog management (MongoDB)
2. **UserService** - User and identity management (SQL Server)
3. **OrderService** - Order management (SQL Server)
4. **PaymentService** - Payment processing (Mock service)
5. **CartService** - Shopping cart management (Redis)
6. **NotificationService** - Notifications (RabbitMQ)

## Architecture Components

- **API Gateway** - Built using Ocelot
- **Databases** - MongoDB, SQL Server, Redis
- **Message Queue** - RabbitMQ
- **Containerization** - Docker
- **Orchestration** - Docker Compose

## Getting Started

### Requirements

- Docker
- Docker Compose

### Installation

1. Clone the project:
   ```
   git clone <repo-url>
   cd ECommercePlatform
   ```

2. Start the Docker containers:
   ```
   docker-compose up -d
   ```

3. It may take a few minutes for the applications to start. Wait for all services to be ready.

### Access Points

- **API Gateway**: http://localhost:5000
- **Catalog Service**: http://localhost:5001
- **User Service**: http://localhost:5002
- **Order Service**: http://localhost:5003
- **Payment Service**: http://localhost:5004
- **Cart Service**: http://localhost:5005
- **Notification Service**: http://localhost:5006
- **MongoDB**: mongodb://localhost:27017
- **SQL Server**: localhost:1433
- **Redis**: localhost:6379
- **RabbitMQ**: localhost:5672 (Management: http://localhost:15672)

### API Documentation

All services are documented with Swagger UI:

- API Gateway: http://localhost:5000/swagger
- Catalog Service: http://localhost:5001/swagger
- User Service: http://localhost:5002/swagger
- Order Service: http://localhost:5003/swagger
- Payment Service: http://localhost:5004/swagger
- Cart Service: http://localhost:5005/swagger
- Notification Service: http://localhost:5006/swagger

## Usage

1. Register with the User Service to create a user
2. Obtain a JWT token
3. View products with the Catalog Service
4. Add products to cart with the Cart Service
5. Create an order with the Order Service
6. Make payment with the Payment Service
7. Check notifications with the Notification Service

## Contribution

This project was created for educational purposes. If you'd like to contribute, you can submit a pull request.
