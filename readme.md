
This is a simple Blog API application built with ASP.NET Core and PostgreSQL. It supports features like posts and comments with both eager and lazy loading configurations.

## Prerequisites

Before running the application, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [PgAdmin (optional)](https://www.pgadmin.org/download/)

---

## Steps to Run the Application

### 1. Clone the Repository
Clone the repository to your local machine:

```bash
git clone <repository-url>
cd <repository-folder>
```
### 2. Restore the Database
1. **Start PostgreSQL:**
   - Make sure your PostgreSQL server is running.
   - Create a new database for the application:

    ```sql 
    CREATE DATABASE blog_api;
    ```
   <br>

2. **Restore the Database:**

   - Use pg_restore to import the database from the provided .sql file:
    ```bash
    psql -U <your-username> -d blog_api -f path/to/exported_file.sql
    ```

   - Replace <your-username> with your PostgreSQL username and adjust the path to the .sql file.
Example:

    ```bash
    psql -U postgres -d blog_api -f database/blog_api_dump.sql
    ```
   <br>

3. **Update the Configuration**
   - Ensure your appsettings.json file has the correct PostgreSQL connection string:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=blog_api;Username=<your-username>;Password=<your-password>"
    }
    ```
   - Replace <your-username> and <your-password> with your PostgreSQL credentials.
     <br><br>
   
4. **Install Dependencies**
   - Navigate to the project folder and restore the NuGet packages:
    ```bash
    dotnet restore 
    ``` 
   <br>

5. **Run the Application**
   - Start the application with the following command:
    ```bash
    dotnet run]
    ```
   - The application should be running at http://localhost:5000 or https://localhost:5001.<br><br>
6. **Test the API**
   - You can test the API using tools like Postman or Swagger UI. Swagger is available at /swagger when the application is running.