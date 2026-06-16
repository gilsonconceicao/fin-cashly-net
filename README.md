FinCashly API

FinCashly is a REST API developed in .NET, focused on personal financial management. It enables control of users, accounts, transactions, categories, and goals, with authentication via Firebase, authorization based on Roles & Permissions, and an architecture following Clean Architecture + CQRS.

### Architecture

The project follows Clean Architecture principles, ensuring separation of concerns, testability, and scalability.
```
src/
 ├── FinCashly.API              → Controllers, Middlewares, Filters, Swagger
 ├── FinCashly.Application      → CQRS (Commands / Queries), DTOs, Interfaces, Security
 ├── FinCashly.Domain           → Entities, Enums, Exceptions, Business Rules
 └── FinCashly.Infrastructure   → EF Core, Repositories, Firebase, Auth, Persistence
 ```
### Applied Concepts
- Clean Architecture
- CQRS (Command Query Responsibility Segregation)
- MediatR
- Unit of Work + Repository
- Memory cache
- AutoMapper
- FluentValidation
- Soft Delete
- Global Exception Handling
- Automatic auditing (CreatedAt, UpdatedAt, CreatedBy)
- Swagger documentation with XML Comments

### Key Entities
- Account
- Transaction
- Category
- Goal

### All entities include:

- Soft Delete
- Auditing
- Explicit relationships
- How to Run the Project
- Running with Docker (recommended)

#### Make sure you have Docker and Docker Compose installed and running.

```
docker-compose up -d --build
```

Stopping the containers
```
docker-compose down
```
Rebuilding the application
```
docker-compose up -d --build
```

Running Locally (without Docker)
Restore dependencies
```
dotnet restore
```
Run the API
```
dotnet run --project src/FinCashly.API
```
