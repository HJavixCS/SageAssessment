# README #

---
### Sage Assessment (Senior Developer) ###

 A solution for the following exercise, developed in any technology. 

**Exercise requirements:**

*          It can be a web, desktop or console solution
+          It must read from any kind of storage a list of invoices with the following data:
    *   Number
    *   Date
    *   Customer
    *   Description
    *   Amount
    *   Status (Pending / Paid)
*          It must present the list of invoices to the user with the total amount of all the invoices.
+          It must provide the option to create a new invoice, requesting to the user:
    *   The date (defaulted to today)
    *   The customer (from a given fixed list)
    *   The description
    *   The Amount
*          It will add the invoice to the storage, with pending status and correlative number
*          It must provide the option to change the status of a given invoice
---

Solution Deployed: http://hjcssageassessmentclientmvc.azurewebsites.net/

Solution by: Héctor Javier Castillo Suazo (https://twitter.com/hjavixcs).

### What is this repository for? ###

* Visual Studio 2017 Solution (ASP.Net Core). C#, Web API, Repository Pattern, Dependency Injection
* Storage: Microsoft.EntityFrameworkCore.InMemory  
* Backend: Web API
* Frontend: MVC Client
* 1.0

### How do I get set up? ###

* Clone repository (or download source code)
* Restore Nuget Packages for the solution
* There is not database configuration (In Memory Database)
* Check if Web API and Client project are set as Startup Project in Properties Solution (HJCS.SageAssessment.WebAPI and HJCS.SageAssessment.ClientMVC)
* Run the solution into Visual Studio

### Who do I talk to? ###

* Repo owner: javier.castillosuazo@gmail.com