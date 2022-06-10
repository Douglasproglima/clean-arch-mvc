<p align="center">
  <img alt="Logo DevRise Week" title="#douglasproglima-apps" src="./assets/images/0-logo.png" width="450px" />
</p>

<h1 align="center">
🚀 Clean Architecture Or  Hexagonal Architecture 🚀
</h1>

## Motivation
___
### Learn how to design modular applications
- Learning how to design modular applications will help you become a better engineer. Designing modular applications is the holy grail of software architecture, it is hard to find engineers experienced in designing applications which allows adding new features at a steady speed.

### Explore the .NET Core features
- .NET Core brings a sweet development environment, an extensible and cross-platform framework. We will explore the benefits of it in the infrastructure layer and we will reduce its importance in the application and domain layers. The same rule is applied for modern C# language syntax.

## Hexagonal Architecture Style
___

The general idea behind Hexagonal architecture style is that the dependencies (Adapters) required by the software to run are used behind an interface (Port).  

The software is divided into **Application** and **Infrastructure** in which the adapters are interchangeable components developed and tested in isolation. The Application is loosely coupled to the Adapters and their implementation details.

#### Ports

Interfaces like `ICustomerRepository`, `IOutputPort` and `IUnitOfWork` are ports required by the application.

#### Adapters

The interface implementations, they are specific to a technology and bring external capabilities. For instance the `CustomerRepository` inside the `EntityFrameworkDataAccess` folder provides capabilities to consume an SQL Server database.

![Ports and Adapters](./assets/images/0-camadas.png)

#### The Left Side

Primary Actors are usually the user interface or the Test Suit.

#### The Right Side

The Secondary Actors are usually Databases, Cloud Services or other systems.

### Onion Architecture Style

Very similar to Ports and Adapters, I would add that data objects cross boundaries as simple data structures. For instance, when the controller execute a use case it passes an immutable Input message. When the use cases calls a Presenter it gives an Output message (Data Transfer Objects if you like).

### Clean Architecture Style

The Clean Architecture style focus on a loosely coupled implementation of use cases and it is summarized as:

1. It is an architecture style that the Use Cases are the central organizing structure.
1. Follows the Ports and Adapters pattern.
   * The implementation is guided by tests (TDD Outside-In).
   * Decoupled from technology details.
1. Follows lots of principles (Stable Abstractions Principle, Stable Dependencies Principle, SOLID and so on).

## Layers Definition This Project
---

Relation and dependency between projects:
![Dependecy Projects](./assets/images/1-project-dependecy.png)

Layer Domain or Entities
![Domain Layer](./assets/images/2-camada-domain.png)

Layer Application or Use Cases
![Domain Application](./assets/images/4-camada-application.png)

Layer Application with Services to Mapper and Convertion Objects
![Domain Application](./assets/images/4.1-camada-application-service.png)

Layer Infra
![Infra Layer](./assets/images/3.0-camada-infraestrutura.png)

Layer Infra.Data
![Domain Infra.Data](./assets/images/3.1-camada-infra-data.png)

Layer Domain X Infra.Data
![Domain Infra.Data](./assets/images/3.2-camada-domain-X-camada-infra-data.png)

Layer Infra.IoC
![Domain Infra.IoC](./assets/images/3.3-camada-infra-IoC.png)

Layer Web.UI
![Domain Infra.IoC](./assets/images/5.0-fluxo-camada-webui-para-infra-data.png)

### Controllers e Views
![WebUI](./assets/images/7-web-ui/1.0-controller-views.png)

### Views: Bootstrap
![WebUI](./assets/images/7-web-ui/1.1-controller-views.png)

## CQRS - Command Query Responsability Separation
---

### Concerns
![CQRS](./assets/images/6.0-CQRS.png)

### CQRS - How to use
![CQRS](./assets/images/6.1-CQRS.png)

### Implementation And When Not To Use
![CQRS](./assets/images/6.2-CQRS-Implementation.png)

### CQRS And Use Mediator Pattern
![CQRS](./assets/images/6.3-CQRS-Mediator-Pattern.png)

### CQRS Implementation Class
![CQRS](./assets/images/6.4-CQRS-Classes.png)

### CQRS Handlers
![CQRS](./assets/images/6.5-CQRS-Handlers.png)

### CQRS Handlers X Mediator Pattern X Requests
![CQRS](./assets/images/6.6-CQRS-Handlers-X-Requests.png)

### CQRS Handlers X Mediator 
![CQRS](./assets/images/6.7-CQRS-Handlers-X-MediatR.png)

### CQRS Fluxo das Requests
![CQRS](./assets/images/6.8-CQRS-fluxo-request.png)

### CQRS Comparando os Fluxos (Sem/Com o CQRS)
![CQRS](./assets/images/6.9-CQRS-fluxo-request-sem-CRQS.png)

### CQRS Considerações finais
![CQRS](./assets/images/6.9.1-CQRS-consideracoes-finais-1.png)

![CQRS](./assets/images/6.9.1-CQRS-consideracoes-finais-2.png)

## Identity Com Clean Archtecture
---

## Solução/Proposta
![Identity](./assets/images/8-identity/1-clean-architecture-identity.png)

## Autenticação
![Identity](./assets/images/8-identity/2-autenticacao-classe-startup.png)

## Autorização/Atributos
![Identity](./assets/images/8-identity/3-autorizacao-atributos.png)

## Recursos p/ Autorização
![Identity](./assets/images/8-identity/4-recursos-autorizacao.png)

## Implementação
* Adicionar ao projeto CleanArchMvc.Infra.Data as referências:

```
    <PackageReference Include="Microsoft.AspNetCore.Identity" Version="2.2.0" />
    <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="5.0.17" />
```

* Criar a classe ApplicationUser que herda de IdentityUser
```
    public class ApplicationUser :IdentityUser { }
```

* Alterar a herança da classe ApplicationDbContext para:
    public class ApplicationUser :IdentityUser { }
```
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> { ... }
```
* Acessar o Nuget Packeg Console, irá criar o arquivo de migration referente ao Identity
```sh
$ add-migration AddIdentityTables
```

* Nuget Packeg Console, rodar o comando abaixo para aplicar a migration criada acima
para refletir no DB.
```sh
$ update-database
```

# Diagrama das Tabelas do Identity
![Identity](./assets/images/8-identity/5-diagrama-db.png)

## Implementação
![Identity](./assets/images/8-identity/6-conceito-final.png)

> Todos os créditos são do professor @Macorrati | 
Feito com ❤️ por Douglas Lima <img src="https://raw.githubusercontent.com/Douglasproglima/douglasproglima/master/gifs/Hi.gif" width="30px"></h2> [Entre em contato!](https://www.linkedin.com/in/douglasproglima)