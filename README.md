# exchange-rates-api
API service that convert currencies based on EUR

##Running the .NET API Application

###Visual Studio

Just open the solution file: ExchangeRates.sln
Choose the IIS Express profile and click Play

##Project porpose

This API project meaning is to make available the latest Exchange Rate based on EUR to other currencies, passing the destination currency and value the API return the destination value in the currency passed.

##Technology

### .Net 5
The latest stable version of .Net that are multiplatform.
### Sqllite
Using SqLite for embedded database to save e get the transactions.

### Entity Framework Core
The most famous and efficienty .Net ORM.

### Layers
**API**
The layer who receive all the requests from outside.

**Domain**
The main layer, who get the application bussines rules.

**Infraestructure**
The layer responsible to access repositories like external API and database access.