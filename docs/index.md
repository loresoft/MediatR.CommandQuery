# MediatR.CommandQuery

CQRS framework using MediatR, AutoMapper and FluentValidation

| Package                                                                                                              | Version                                                                                                                                                                                              | Description                                                      |
| :------------------------------------------------------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :--------------------------------------------------------------- |
| [MediatR.CommandQuery](https://www.nuget.org/packages/MediatR.CommandQuery/)                                         | [![MediatR.CommandQuery](https://img.shields.io/nuget/v/MediatR.CommandQuery.svg)](https://www.nuget.org/packages/MediatR.CommandQuery/)                                                             | Base package for Commands, Queries and Behaviours                |
| [MediatR.CommandQuery.EntityFrameworkCore](https://www.nuget.org/packages/MediatR.CommandQuery.EntityFrameworkCore/) | [![MediatR.CommandQuery.EntityFrameworkCore](https://img.shields.io/nuget/v/MediatR.CommandQuery.EntityFrameworkCore.svg)](https://www.nuget.org/packages/MediatR.CommandQuery.EntityFrameworkCore/) | Entity Framework Core handlers for the base Commands and Queries |
| [MediatR.CommandQuery.MongoDB](https://www.nuget.org/packages/MediatR.CommandQuery.MongoDB/)                         | [![MediatR.CommandQuery.MongoDB](https://img.shields.io/nuget/v/MediatR.CommandQuery.MongoDB.svg)](https://www.nuget.org/packages/MediatR.CommandQuery.MongoDB/)                                     | Mongo DB handlers for the base Commands and Queries              |
| [MediatR.CommandQuery.Endpoints](https://www.nuget.org/packages/MediatR.CommandQuery.Endpoints/)                     | [![MediatR.CommandQuery.Endpoints](https://img.shields.io/nuget/v/MediatR.CommandQuery.Endpoints.svg)](https://www.nuget.org/packages/MediatR.CommandQuery.Endpoints/)                               | Minimal API endpoints for base Commands and Queries              |
| [MediatR.CommandQuery.Mvc](https://www.nuget.org/packages/MediatR.CommandQuery.Mvc/)                                 | [![MediatR.CommandQuery.Mvc](https://img.shields.io/nuget/v/MediatR.CommandQuery.Mvc.svg)](https://www.nuget.org/packages/MediatR.CommandQuery.Mvc/)                                                 | MVC Controller endpoints base Commands and Queries               |

## Features

- Base commands and queries for common CRUD operations
- Generics base handlers for implementing common CRUD operations
- Common behaviors for audit, caching, soft delete, multi-tenant and validation
- View modal to data modal mapping via AutoMapper
- Model validation via FluentValidation
- Entity Framework Core handlers for common CRUD operations
- MongoDB handlers for common CRUD operations

## Installation

The MediatR.CommandQuery.EntityFrameworkCore library is available on nuget.org via package name `MediatR.CommandQuery.EntityFrameworkCore`.

To install MediatR.CommandQuery.EntityFrameworkCore, run the following command in the Package Manager Console

```powershell
Install-Package MediatR.CommandQuery.EntityFrameworkCore
```

OR

```shell
dotnet add package MediatR.CommandQuery.EntityFrameworkCore
```
