# NayraBase

Sistema de gestión con RBAC jerárquico desarrollado con .NET 10 y PostgreSQL.

## Stack Tecnológico

- .NET 10.0
- ASP.NET Core Web API
- Entity Framework Core 10.0
- PostgreSQL 18.1
- JWT Authentication
- AutoMapper
- FluentValidation

## Arquitectura

- **Domain**: Entidades y lógica de dominio
- **Application**: Servicios, DTOs, validaciones
- **Infrastructure**: EF Core, repositorios, seguridad
- **API**: Controllers, middleware, filtros

## Configuración

1. PostgreSQL debe estar corriendo en localhost:5432
2. Usuario: `nayrabase_user`
3. Base de datos: `nayrabase_db`
4. Ejecutar migraciones: `dotnet ef database update`

## Ejecutar
```bash
cd src/NayraBase.API
dotnet run
```

API disponible en: https://localhost:5001
Swagger: https://localhost:5001/swagger
```

---

## 📁 ESTRUCTURA FINAL
```
NayraBase/
│
├── global.json                       ← Fuerza .NET 10.0.103
├── NayraBase.sln
├── .gitignore
├── README.md
│
├── src/
│   ├── NayraBase.Domain/            ← net10.0
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Interfaces/
│   │
│   ├── NayraBase.Application/       ← net10.0
│   │   ├── DTOs/
│   │   ├── Services/
│   │   ├── Mappings/
│   │   ├── Validators/
│   │   └── Common/
│   │
│   ├── NayraBase.Infrastructure/    ← net10.0
│   │   ├── Data/
│   │   ├── Repositories/
│   │   ├── Security/
│   │   ├── UnitOfWork/
│   │   └── Extensions/
│   │
│   └── NayraBase.API/               ← net10.0
│       ├── Controllers/
│       ├── Middleware/
│       ├── Filters/
│       ├── Extensions/
│       ├── appsettings.json
│       └── Program.cs
│
└── tests/
    ├── NayraBase.UnitTests/         ← net10.0
    └── NayraBase.IntegrationTests/  ← net10.0