# InsuranceApp - Server

A learning project implementing a layered ASP.NET Core Web API for managing customers and insurance records. Designed to demonstrate practical knowledge of modern .NET development, clean architecture principles, and API best practices.

## Features
- Layered architecture: API layer, Manager/Service layer, Repository/Data layer
- RESTful API design with controllers and DTOs
- Dependency Injection and interface-driven design
- Asynchronous programming with async/await
- DTO mapping using AutoMapper
- Authorization (JWT / role/policy-based) applied to endpoints
- Repository pattern and separation of concerns
- Clear, testable code structure suitable for extension (unit/integration tests)

## Technologies used
- .NET 8 / C# 12
- ASP.NET Core Web API (Controllers, Routing, Model Binding)
- AutoMapper for model ⇄ DTO mapping
- Repository pattern (data access abstraction)
- Entity Framework Core
- Authorization attributes and policies
- Async programming patterns

## Project structure
- `InsuranceApp.Api` - API project (controllers, DTOs, program startup, business logic & mapping)
- `InsuranceApp.Data` - data models, repositories

## Learning outcomes

This project highlights skills in:

Building scalable ASP.NET Core backends with layered architecture.
Implementing authentication and authorization (policy-based / JWT).
Using AutoMapper and separating DTOs from domain models.
Designing RESTful API with consistent endpoints.
 

