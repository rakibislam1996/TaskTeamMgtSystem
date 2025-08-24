# TaskTeamMgtSystem

A Task and Team Management System built with ASP.NET Core Web API, implementing Clean Architecture, CQRS pattern, and role-based access control.

## Features

- **CRUD Operations** for Users, Teams, Tasks, and User-Team mappings
- **Role-based Access Control**: Admin, Manager, Employee permissions
- **Task Filtering** by status, assignee, team, and due date with pagination
- **JWT Authentication** with secure token-based authorization
- **CQRS Pattern** with MediatR and FluentValidation
- **Global Exception Handling** and centralized logging with Serilog

## Tech Stack

- .NET 8.0, Entity Framework Core, SQL Server
- JWT Authentication, FluentValidation, Serilog
- xUnit Testing, Swagger/OpenAPI
- Clean Architecture, CQRS, Repository Pattern

## Quick Start

1. **Clone and setup**
   ```bash
   git clone https://github.com/rakibislam1996/TaskTeamMgtSystem.git
   cd TaskTeamMgtSystem
   dotnet restore
   ```

2. **Run application**
   ```bash
   cd TaskTeamMgtSystem.API
   dotnet run
   ```
   
3. **Access**
   - API: `https://localhost:7249`
   - Swagger: `https://localhost:7249/swagger`

*Database creates automatically on first run*

## Default Users

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@demo.com | Admin123! |
| Manager | manager@demo.com | Manager123! |
| Employee | employee@demo.com | Employee123! |

## API Endpoints

### Authentication
- `POST /login` - User authentication

### Users (Admin only)
- `GET/POST/PUT/DELETE /api/user` - User management

### Teams (Admin only)
- `GET/POST/PUT/DELETE /api/team` - Team management

### Tasks
- `GET /api/taskitem` - Get tasks (with filtering, pagination)
- `POST /api/taskitem` - Create task (Manager+)
- `PUT /api/taskitem` - Update task (Manager+)
- `DELETE /api/taskitem/{id}` - Delete task (Manager+)
- `PUT /api/taskitem/employee/update` - Update own task (Employee)

### User-Team Mapping (Admin only)
- `GET/POST/DELETE /api/userteammapping` - Manage user-team assignments

## Testing

```bash
# Run tests
dotnet test

# Manual testing via Swagger
1. Navigate to /swagger
2. Login with default credentials
3. Copy JWT token
4. Click "Authorize" → Enter: Bearer <token>
5. Test endpoints
```

## Project Structure

```
├── TaskTeamMgtSystem.API/          # Web API Controllers
├── TaskTeamMgtSystem.Application/   # CQRS, Services, Validation
├── TaskTeamMgtSystem.Core/          # Domain Entities, Enums
├── TaskTeamMgtSystem.Infrastructure/# EF Core, Database, Repositories
└── TaskTeamMgtSystem.Tests/         # Unit Tests
```

## Configuration

Database connection in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskTeamMgtSystemDb;Trusted_Connection=True"
}
```

JWT settings:
```json
"Jwt": {
  "Key": "YourSuperSecretKey12345!@#",
  "Issuer": "TaskTeamMgtSystemIssuer",
  "Audience": "TaskTeamMgtSystemAudience"
}
```