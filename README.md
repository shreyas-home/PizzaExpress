PizzaExpress - Online Pizza ordering website
C# , .NET6 Web app MVC , Microsoft SQL Server , Repository Design Pattern , Entity Framework , Dependency Injections , LINQ , Visual Studio 2022

-Users can register/login into system , add items into cart , place order and see order history.
-Admin can view,add,update,delete  all items present in system and all orders placed through system.

Repository Design Pattern
-Project will have 4 different layers
  -UI(web) - presentation layes where UI code and UI operations are written.(ASP.NET Web Application Model-View-Controller)
  -Services - communicats with UI layes for input , performs business logic.(Class Library)
  -Repository - Gets input from Services and performs DB operations with Entity Framework.(Class Library)
  -Entities - Models/Classes representing DB tables.(Class Library)
