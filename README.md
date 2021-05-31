# exchange-rates-api
API service that convert currencies based on EUR

## Running the .NET API Application

### Visual Studio

Just open the solution file: ExchangeRates.sln.
Choose the IIS Express profile and press F5 or click in the green play icon at the Visual Studio Toolbar.

## Project porpose

This API project meaning is to make available the latest Exchange Rate based on EUR to other currencies, passing the destination currency and value the API return the destination value in the currency passed.

##Technologies

### .Net 5
The latest stable version of .Net that are multiplatform. 
Has excelent performance to proccess amonts of request better than very famous platforms, like Node.js.
It is the fastest popular platform.
Native Dependency Injection.

### SQLite
SQLite for embedded database to save e get the transactions.
It is based in files and very easy to setup.
Easily transportable.
SQL syntax.
Easy to use. 

### Entity Framework Core
The most famous and efficienty .Net ORM. 
EF Core helps to increase the produtivity
A very fast way to access the database just create some classes and run.
Mapper thedatabase tables to entities.
Querying using LINQ and Lambda expressions.

### Swagger
The best and fastest way to documentation the API
.Net 5 API has already native swagger configuration.

### Layers

**API**
The layer who receive all the requests from outside.

**Domain**
The main layer, who get the application bussines rules.

**Infraestructure**
The layer responsible to access repositories like external API and database access.