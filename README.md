FinCashly API

FinCashly is a REST API developed in **.NET**, focused on personal financial management, enabling control of users, accounts, transactions, categories, and goals, with authentication via **Firebase**, authorization based on Roles & Permissions, and an architecture following **Clean Architecture + CQRS.**

---

## Arquitetura

The project follows **Clean Architecture** principles, ensuring separation of concerns, testability, and scalability.

```
src/
 ├── FinCashly.API              → Controllers, Middlewares, Filters, Swagger
 ├── FinCashly.Application      → CQRS (Commands / Queries), DTOs, Interfaces, Security
 ├── FinCashly.Domain           → Entidades, Enums, Exceptions, Regras de Negócio
 └── FinCashly.Infrastructure   → EF Core, Repositórios, Firebase, Auth, Persistence
```

---

## Patterns Used

- Clean Architecture  
- CQRS (Command Query Responsibility Segregation)  
- MediatR  
- Unit of Work + Repository  
- AutoMapper  
- FluentValidation  
- Soft Delete  
- Global Exception Handling  
- Auditoria automática (CreatedAt, UpdatedAt, CreatedBy)  
- Swagger documentado com XML Comments  

---
## Entities Key

- User  
- Account  
- Transaction  
- Category  
- Goal  

Todas com:
- Soft Delete  
- Auditoria  
- Relacionamentos explícitos  

