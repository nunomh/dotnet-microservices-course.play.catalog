# .NET Microservices Full Course for Beginners  

Based on: https://www.youtube.com/watch?v=CqCDOosvZIk&ab_channel=freeCodeCamp.org  

Software:
- Docker Desktop
- VS Code
- .NET 5.0

### Project Creation  

``dotnet new webapi -n Play.Catalog.Service --framework net5.0``  

### Project Build and Run

inside project folder (Play.Catalog.Service)  
``dotnet build``  
``dotnet run``  

in case you have a ssl error: ``dotnet dev-certs https --trust``  

### Theory Notes   

#### Catalog REST API:  
The REST API defines the operations exposed by the microservice. FI:  
- GET /catalogitems: returns a list of all catalog items
- GET /catalogitems/{id}: returns a specific catalog item by id
- POST /catalogitems: creates a new catalog item
- PUT /catalogitems/{id}: updates a specific catalog item by id
- DELETE /catalogitems/{id}: deletes a specific catalog item by id
  
#### Data Transfer Objects (DTOs):  
A Data Transfer Object (DTOs) is an object that carries data between processes. FI:  
- DTO is the payload return to the client {id=123, Name="Potion", Price=9}
The DTO represents the contract between the microservice API and the 

#### Controllers
The controller is the entry point for incoming HTTP requests. FI:  
- CatalogController.cs: handles HTTP requests to the Catalog REST API  