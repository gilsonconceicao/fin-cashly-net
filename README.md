# FinCashly API

**FinCashly** é uma API REST desenvolvida em **.NET**, voltada para o gerenciamento financeiro pessoal, permitindo controle de **usuários, contas, transações, categorias e metas**, com autenticação via **Firebase**, autorização baseada em **Roles & Permissions**, e arquitetura **Clean Architecture + CQRS**.

---

## Arquitetura

O projeto segue os princípios de **Clean Architecture**, garantindo separação de responsabilidades, testabilidade e escalabilidade.

```
src/
 ├── FinCashly.API              → Controllers, Middlewares, Filters, Swagger
 ├── FinCashly.Application      → CQRS (Commands / Queries), DTOs, Interfaces, Security
 ├── FinCashly.Domain           → Entidades, Enums, Exceptions, Regras de Negócio
 └── FinCashly.Infrastructure   → EF Core, Repositórios, Firebase, Auth, Persistence
```

---

## Padrões Utilizados

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
## Principais Entidades

- User  
- Account  
- Transaction  
- Category  
- Goal  

Todas com:
- Soft Delete  
- Auditoria  
- Relacionamentos explícitos  

