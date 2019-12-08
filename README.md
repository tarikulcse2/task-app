# Task-App ASP.NET WEB API and Angular 8

This example shows how to use ASP.NET Core 2.2 WEB API MVC.


**Prerequisites:** 
[.NET Core SDK 2.2](https://dotnet.microsoft.com/download) or higher.
[NodeJS](https://nodejs.org/en/) || 
[Angular CLI](https://angular.io/cli) ||
[Sql Server 2017](https://www.microsoft.com/en-us/sql-server/sql-server-2017)

**Code Editing:** 
[Visual Studio Code](https://code.visualstudio.com/) ||
[Visual Studio 2017](https://visualstudio.microsoft.com/downloads/)

## Getting started
To install this example application, clone this repository with Git:

```bash
git clone https://github.com/tarikulcse2/task-app.git
```

## Database Connection
Update the `appsettings.json` file with these values:
* `ConnectionStrings` - change database name and server name

```bash
execute
sqlquery.sql file to sql server
```
### Server-Side Application Run
```bash
cd server
dotnet restore
cd Test.WebApi
dotner run
```
dotnet core server run http://localhost:5005 

### Client-Side Application Run
```bash
cd client
npm install
ng serve or npm start
```
angular server run http://localhost:4200 

### Start the application

Browse to `http://localhost:4200` to test the application.

