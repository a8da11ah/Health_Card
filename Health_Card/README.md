# Health_Card API

This project is a .NET Core Web API for managing health card information. It provides endpoints for managing servants, chronic diseases, medical referrals, and other health-related data.

## Project Structure

The project is structured as follows:

- `Controllers/`: Contains the API controllers that handle incoming HTTP requests.
- `Services/`: Contains the business logic of the application.
- `Repositories/`: Contains the data access logic, using Dapper to interact with the database.
- `Models/`: Contains the database models.
- `DTOs/`: Contains the data transfer objects used to transfer data between the client and the server.
- `Interfaces/`: Contains the interfaces for the services and repositories.
- `Data/`: Contains the Dapper context and other data-related classes.
- `SQL code/`: Contains the SQL scripts for creating the database schema and stored procedures.

## Getting Started

To get started with the project, you will need to have the following installed:

- .NET 8 SDK
- SQL Server

Once you have the prerequisites installed, you can follow these steps to run the project:

1. Clone the repository to your local machine.
2. Create a new database in SQL Server.
3. Run the SQL scripts in the `SQL code/` folder to create the database schema and stored procedures.
4. Open the `appsettings.json` file and update the connection string to point to your database.
5. Open a terminal in the project root and run the following command to start the application:

```
dotnet run
```

The API will be available at `http://localhost:5000`.