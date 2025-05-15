# Etalent C# .NET Assessment

This is a simple .NET Core Web API developed as part of the Etalent C# assessment. The project demonstrates key backend development principles including RESTful APIs, data validation, repository pattern, dependency injection, and unit testing using xUnit and Moq.

---
## 🚀 Features

- ✅ Account retrieval by holder ID or account number
- ✅ Withdrawal operation with business validation rules
- ✅ Login That generate JWT for API usage
- ✅ Proper use of HTTP status codes
- ✅ Repository pattern and dependency injection
- ✅ Unit testing with mocked repository
# HOW TO RUN Application
     pull the code
     remove current migrations 
          -- run on Terminal 
          : rm -r Migrations
     Create new migration
          --run on Terminal
          dotnet ef migrations add InitialCreate
          dotnet ef database update
# RUN THE APPLICATION
     IF USING VSCODE
       run   Terminal
          : dotnet run

# HOW TO RUN TESTS
    run on Terminal 
           dotnet test
  ## 🛠️ Technologies Used

- C# [.NET 8]
- ASP.NET Core Web API
- xUnit for testing
- Moq for mocking dependencies
- Entity Framework
- SQL server
- Postman(Manual testing )

