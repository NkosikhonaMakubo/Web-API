# Project-2-Web-API-32815069-
Web API Development

### About the Project:
This project is a Web API that works with a SQL Server database called `cmpg323projdb-dev`, hosted on Azure.

#### The database has four tables:
- **Config.Client**
- **Config.Process**
- **Config.Project**
- **Telemetry.JobTelemetry**

#### Microsoft Azure database
- Created an account on Microsoft Azure.
- Set a monthly budget to avoid exceeding credits.
- Created a resource group named `rgProject2API`.
- Set up a SQL server and database under that resource group.

### API Creation
- Developed the web API using Visual Studio.
- Added controllers to manipulate tables in the Azure-hosted database.
- Implemented security to restrict unauthorized access.

### Hosting the API
- Published the API via Visual Studio and deployed it on Azure.

### Security:
The API is secured to ensure that only authorized users can access it. Users must register an account to manipulate the database. The system uses token-based authentication, and sensitive information (like json files) is protected using a `.gitignore` file to exclude these details from GitHub.

## API Usage Guide

### Getting Started
To interact with the API, follow these steps:

#### Register an Account:
- Visit the API registration page to create an admin account. This account is necessary for accessing and manipulating database tables.

#### Log In:
- Use your registration credentials to log in. Upon successful login, you’ll receive an authentication token.

### Using the API
- Here’s a list of available API endpoints:

#### Endpoints

**JobTelemetry**
- **GET** `/api/JobTelemetries: Retrieves a list of all job telemetry entries.
- **GET** `/api/JobTelemetries/{id}: Retrieves a specific job telemetry entry by ID.
- **POST** `/api/JobTelemetries: Creates a new job telemetry entry.
- **PUT** `/api/JobTelemetries/{id}: Updates an existing job telemetry entry by ID.
- **DELETE** `/api/JobTelemetries/{id}: Deletes a specific job telemetry entry by ID.

**Custom Endpoints**
- **GET** `/api/JobTelemetries/GetSavingsByProject/{projectId}: Retrieves cumulative time and cost savings for a specific project.
- **GET** `/api/JobTelemetries/GetSavingsByClient/{clientId}: Retrieves cumulative time and cost savings for a specific client.

### Authentication
- Include the authentication token in the Authorization header for each request.
  - Example: `Authorization: Bearer your-token-here`

### Example Workflow
1. Register an admin account.
2. Log in to receive the authentication token.
3. Use the token in the Authorization header for subsequent requests.
4. Access JobTelemetries data using the provided endpoints.
5. Make `GET`, `POST`, `PUT`, or `DELETE` requests as needed.

### Security Notes
- The API uses token-based authentication, restricting access to admin users.
- The server hosting the database is secured, and sensitive data is protected.

## Resource Group
[Azure Resource Group Overview](https://portal.azure.com/#@nwuac.onmicrosoft.com/resource/subscriptions/8f26bbf4-d9cb-4d12-a4c9-65fc4a883104/resourceGroups/rgProject2API/overview)

## Link to API
[API Endpoint](https://restapiproject20240812150408.azurewebsites.net/swagger/index.html)
