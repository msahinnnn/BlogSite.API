# BlogSite.API

A blog application developed using layered architecture.

Core Layer:
- Generic abstract structures were created for the implementation of services to be used in layers within the project.
- All services have been created generic so that they can be injected directly into other projects.
- In this layer, the repository services to be used in the DataAccess Layer were created generically. Validation Tool was written for ViewModels created in Entity Layer and error management was done. A generic response structure to be used during the use of services has been created.

Entities Layer:
- Three base entities were used.
- ViewModels were created for CRUD operations.

DataAccess Layer:
- The concrete versions of the generic service structures created in the Core Layer were created in accordance with each entity.
- It was decided to use the EF Core migration structure for fast creation of the database. The DbContext structure has been created.
- ADO.NET was used for database access and CRUD operations in the Repository created for each entity.
- In order to show the loose coupled feature of the system, the Repository structure is also shown using Dapper.

Business Tier:
- The concrete versions of the generic service structures created in the Core Layer were created in accordance with each entity.
- Business codes were written according to the features of the project.
- Concrete structures have been created for the Cache Microservice, which will undertake the Create, Update and Delete operations of the Post and Comment entities.
- Cache Service was created to read the data in the cache directly from the cache.
- AutoMapper library was used to transform ViewModels and validation of requests was created with ValidationTool created in Core Layer.
- Constant class has been created for the response messages that will return to the incoming requests and the code has been purified from texts.
- TokenHandler class has been created for authentication operations. AuthService has been created for Login, Register and token operations.
- A logging mechanism was created with ElasticSearch and Kibana to track errors.

API:
- Classic Web API is used.
- The services in the Business Layer were used in APIs with dependency injection.
- RabbitMQ Masstransit structure has been created to communicate with the Cache Microservice, which will undertake the Create, Update and Delete operations of the Post and Comment entities. With the Queue structure, Create, Update and Delete operations were done via Cache Microservice.

Caching Microservice:
- With the RabbitMQ system, Create, Update and Delete requests from WebAPI were distributed to Consumers with MassTransit.
- Generic cache services and generic repository using Dapper were used.
- Incoming requests were handled with services made in MassTransit consumers.

Test:
- Unit tests were written using XUnit Framework and FakeItEasy.Test:
