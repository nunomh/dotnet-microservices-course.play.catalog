# .NET Microservices Full Course for Beginners  

Based on: https://www.youtube.com/watch?v=CqCDOosvZIk&ab_channel=freeCodeCamp.org   (paused at 2h33m)

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


### Docker

To create a mongoDB image, run the command: ``docker run -d --rm --name mongoPlayTest -p 27017:27017 -v mongodbdata:/data/db mongo``  
if mongo is not already installed, this will pull and install all the packages needed to create a mongoDB image and run that image.

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
This is where you can define required fields or range of values for fields.  


#### Controllers
The controller is the entry point for incoming HTTP requests. FI:  
- CatalogController.cs: handles HTTP requests to the Catalog REST API  


#### Repository
A  repository is a layer of abstraction between the controller and the data storage. FI:
- Application Logic -> Items Repository -> Data Storage (Database)  
If for instance, the database needs to change, the only  thing that needs to change is the repository. Not the entire application.  
In this projects is represented by the Entities folder.  

In the context of .NET API development, especially when working with web applications and services like ASP.NET Core, the terms "entities" and "models" are often used, and while they can sometimes overlap, they serve distinct purposes.

## 1. **Entities**
Entities typically represent **domain-level objects** that map directly to database tables. These are part of your data access layer and are used in object-relational mapping (ORM) frameworks like Entity Framework (EF). They contain the properties that correspond to the database columns and might also include relationships between entities.

**Example:**
```csharp
public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    // Navigation property
    public CategoryEntity Category { get; set; }
}
```

## 2. **Models**
Models typically refer to **DTOs (Data Transfer Objects)** or **view models**, and they are used to **transfer data** between different layers of the application or to shape data specifically for views in the presentation layer (e.g., in an MVC controller). Unlike entities, models don't usually map directly to database tables, and they might be a subset of or a combination of various entities.

**Types of Models:**
- **DTOs (Data Transfer Objects)**: Used to transfer data between layers or over the network in APIs. These are optimized for what the API or view requires rather than the database schema.
- **View Models**: Used in the presentation layer to pass data from the controller to the view. These models are shaped specifically for the UI requirements.


**Example (DTO):**
```csharp
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
}
```

**Example (View Model):**
```csharp
public class ProductViewModel
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
}
```

## Key Differences

| Aspect          | Entities                                  | Models                                  |
|-----------------|-------------------------------------------|-----------------------------------------|
| **Purpose**     | Represent database tables for persistence | Transfer data between layers or shape data for views |
| **Location**    | Typically in the Data Access Layer (DAL)   | Typically in the Application or Presentation Layer |
| **ORM Mapping** | Mapped to database tables (e.g., EF Core)  | Not directly mapped to the database     |
| **Structure**   | Reflects the database schema               | Optimized for business logic or presentation needs |
| **Dependency**  | Tightly coupled with database structure    | Loosely coupled with database, focused on API/UI needs |

## When to Use Entities vs Models
- **Entities** are ideal for interactions with the database, such as creating, reading, updating, and deleting (CRUD) operations.
- **Models** are used when you need to **separate concerns** and transfer only the relevant data to the client (e.g., via an API) or present it in the UI.

