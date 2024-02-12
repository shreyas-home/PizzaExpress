PizzaExpress - Online Pizza ordering website
C# , .NET6 Web app MVC, Microsoft SQL Server, Repository Design Pattern, Entity Framework, Dependency Injections, LINQ, Visual Studio 2022

-Users can register/login to the system, add items to the cart, place orders, and see order history.
-Admin can view, add, update, and delete  all items present in the system and all orders placed through the system.

Repository Design Pattern
-The project will have 4 different layers
  	-UI(web) - presentation layer where UI code and UI operations are written. (ASP.NET Web Application Model-View-Controller)
  	-Services - communicates with UI layer for input, performs business logic. (Class Library)
  	-Repository - Gets input from Services and performs DB operations with Entity Framework. (Class Library)
  	-Entities - Models/Classes representing DB tables. (Class Library)

Entity Framework
	-Use Migration to create database and tables.
	-We have created models/classes for tables in code and we have create DBContext class with that models (add public Dbset for models and override onconfiguring method-add DB connection string)
 	-run the command in the package manager console "add-migration initial" and then "update-database"
