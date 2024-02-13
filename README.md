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
	-Use Migration to create databases and tables.
	-We have created models/classes for tables in code and we have created a DBContext class with those models (add public Dbset for models and override onconfiguring method-add DB 		connection string)
 	-run the command in the package manager console "add-migration initial" and then "update-database"

Dependency Injections
	-Used to develop loosely coupled software components so code becomes more maintainable.
 	-We have used constructor injection implementation.
  	For example: If we want to use payment gateways in the system, we can have multiple choices like Razorpay, PayPal, etc.
   		-We will create an interface with common payment methods like MakePayment(), GetPaymentId(), etc.
     		-Then we will implement the interface according to payment gateway APIs.
       		-We will pass the interface object in our service class constructor as a parameter and use that to call methods.
	 	-In program.cs file we have to register our interface with implemented class(builder.Services.AddTransient<IPaymentService, RazorPayService>()) , so code will call 			implemented class method
   		-In the future, if we want to change our payment gateway to Paypal, then we just need to create a new class and implement a payment interface using PayPal APIs, and register this injection in program.cs
     		-This way, we do not have to change any existing code for a change.

Improvements
	-UI changes (inconsistent UI, dropdown menu)
 	-Handle Errors and exceptions
  	-Payment system.
