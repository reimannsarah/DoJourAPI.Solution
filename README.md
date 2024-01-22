# DoJourAPI.Solution

### By Sarah Reimann 

## Description

A database to hold journal entries. This API is hosted on Azure and is intended to run with the frontend React webapp [DoJour](https://github.com/reimannsarah/DoJour).

## Technologies Used

* C#
* .NET
* .ASP.NET CORE
* MVC
* Entity Framework Core
* EF Core Migrations
* Swashbuckle
* Swagger
* MySQL

## Database Structure

#### Entries Table

| EntryId | Title  | Subject |  Date  |  Text  |
|---------|--------|---------|--------|--------|
|   int   | string | string  | string | string |



## Instructions to set up database
1. Clone this repo.
2. Open your terminal (e.g. Terminal or GitBash) and navigate to this project's directory called "DoJourAPI".
3. Set up the project:
  * Create a file called 'appsettings.json' in SarahsBdayApi.Solution/SarahsBdayApi directory
  * Add the following code to the appsettings.json file:
  ```
  {
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=do-jour-api;uid=[YOUR_SQL_USER_ID];pwd=[YOUR_SQL_PASSWORD];"
    }
  }
  ```
  * Make sure to plug in your SQL user id and password at ```[YOUR_SQL_USER_ID]``` and ```[YOUR_SQL_PASSWORD]```
4. Set up the database:
  * Make sure EF Core Migrations is installed on your computer by running ```dotnet tool install --global dotnet-ef --version 6.0.0```
  * In the production folder of the project (DoJourAPI.Solution/DoJourAPI) run:
  ```
  dotnet ef database update
  ```
  * You should see the new schema in your _Navigator > Schemas_ tab of your MySql Workbench on refresh called ```do-jour-api```


## Using This Api

* Endpoints for **v1.0** are as follows:

```
DoJourAPI:
GET https://do-jour-api.azurewebsites.net/api/entries
GET https://do-jour-api.azurewebsites.net/api/entries/{id}
POST https://do-jour-api.azurewebsites.net/api/entries
PUT https://do-jour-api.azurewebsites.net/api/entries/{id}
DELETE https://do-jour-api.azurewebsites.net/entries/{id}

POST https://do-jour-api.azurewebsites.net/api/users/login
POST https://do-jour-api.azurewebsites.net/api/users/register

```
# To test routes in Swagger: 

* In your terminal, navigate to the project directory and run ```dotnet watch run``` 
* In your broswer, open https://localhost:5000/swagger/index.html
* use the GUI to navigate the API

# To test routes in PostMan:

* Make sure that Postman API Platform is downloaded to your device
* Start a new request by clicking the + at the top of the window
* Copy and paste any of the above listed end point links into the text bar that says 'Enter URL or paste text'
* Make sure the method to the left of the text box matches the method described for the endpoint you are testing
* If route requires a body, navigate to the body window just below the text box
* Copy and paste the body code listed above and replace fields with their respective values
* Click send and wait for response at the bottom of the window

## Known bugs
