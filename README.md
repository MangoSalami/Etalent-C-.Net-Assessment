# Etalent C# .NET Assessment

This is a simple .NET Core Web API developed as part of the Etalent C# assessment. The project demonstrates key backend development principles including RESTful APIs, data validation, repository pattern, dependency injection, and unit testing using xUnit and Moq.

---
# HOW TO RUN Application
--pull the code
--remove current migrations 
     run on Terminal 
         rm -r Migrations
--Create new migrations
    run on Terminal 
           dotnet ef migrations add InitialCreate
           dotnet ef database update
RUN THE APPLICATION

# HOW TO RUN TESTS
    run on Terminal 
           dotnet test


