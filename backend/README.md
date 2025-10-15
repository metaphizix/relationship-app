# Relationship App - Backend

A comprehensive relationship app backend built with ASP.NET Core 9, Entity Framework Core, PostgreSQL, and SignalR.

## Features

- ✅ User Authentication (JWT with BCrypt password hashing)
- ✅ Couple Management (Create/Join with invite codes)
- ✅ Likes & Dislikes with Reveal Logic
- ✅ Personality Tests
- ✅ Mood Tracking
- ✅ Memory Board
- ✅ Honesty Game (Question Cards)
- ✅ Anonymous Feedback
- ✅ Goals & Progress Tracking
- ✅ Streaks & Gamification
- ⏳ Real-time Communication (SignalR)
- ⏳ AI Coach Integration

## Tech Stack

- **Framework**: ASP.NET Core 9
- **Database**: PostgreSQL 16
- **ORM**: Entity Framework Core 9
- **Authentication**: JWT Bearer Tokens
- **Real-time**: SignalR
- **Logging**: Serilog
- **API Documentation**: Swagger/OpenAPI
- **Validation**: FluentValidation
- **Mapping**: AutoMapper

## Project Structure

```
backend/
├── RelationshipApp.Api/          # Web API Controllers & DTOs
├── RelationshipApp.Core/         # Domain Entities
├── RelationshipApp.Infrastructure/  # EF Core, DbContext, Migrations
├── RelationshipApp.Services/     # Business Logic & Services
└── docker-compose.yml           # Docker configuration
```

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for PostgreSQL)
- [PostgreSQL](https://www.postgresql.org/download/) (if not using Docker)

### Installation Steps

1. **Clone the repository**

   ```powershell
   cd "c:\Developer\Mobile App\backend"
   ```

2. **Start PostgreSQL with Docker**

   ```powershell
   docker-compose up -d
   ```

   This will start:

   - PostgreSQL on `localhost:5432`
   - pgAdmin on `localhost:5050`

3. **Run Database Migrations**

   ```powershell
   dotnet ef database update -p RelationshipApp.Infrastructure -s RelationshipApp.Api
   ```

4. **Run the API**

   ```powershell
   cd RelationshipApp.Api
   dotnet run
   ```

5. **Access Swagger UI**
   Open your browser and navigate to:
   - https://localhost:5001/swagger (HTTPS)
   - http://localhost:5000/swagger (HTTP)

## Configuration

Update `appsettings.Development.json` to configure:

- **Database Connection**: `ConnectionStrings:DefaultConnection`
- **JWT Settings**: `JwtSettings` section
  - `SecretKey`: Change this in production!
  - `AccessTokenExpirationMinutes`: Token lifetime
  - `RefreshTokenExpirationDays`: Refresh token lifetime

## Database Management

### Using Docker Compose

```powershell
# Start PostgreSQL
docker-compose up -d postgres

# Stop PostgreSQL
docker-compose down

# View logs
docker logs relationshipapp_postgres
```

### Using pgAdmin

Access pgAdmin at `http://localhost:5050`:

- Email: `admin@relationshipapp.com`
- Password: `admin`

### Creating Migrations

```powershell
# Create a new migration
dotnet ef migrations add MigrationName -p RelationshipApp.Infrastructure -s RelationshipApp.Api

# Apply migrations
dotnet ef database update -p RelationshipApp.Infrastructure -s RelationshipApp.Api

# Remove last migration
dotnet ef migrations remove -p RelationshipApp.Infrastructure -s RelationshipApp.Api
```

## API Endpoints

### Authentication

- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user
- `POST /api/auth/refresh` - Refresh access token

### Example Request

**Register:**

```json
POST /api/auth/register
{
  "email": "user@example.com",
  "password": "SecurePassword123!",
  "displayName": "John Doe"
}
```

**Response:**

```json
{
  "userId": "guid",
  "email": "user@example.com",
  "displayName": "John Doe",
  "token": "jwt-token-here",
  "refreshToken": "refresh-token-here"
}
```

## Development

### Build Solution

```powershell
dotnet build
```

### Run Tests

```powershell
dotnet test
```

### Clean Solution

```powershell
dotnet clean
```

## Database Schema

The application uses the following main tables:

- `users` - User accounts
- `couples` - Couple relationships
- `couple_members` - Many-to-many relationship between users and couples
- `likes_dislikes` - Likes and dislikes entries
- `personality_tests` - Personality test results
- `moods` - Mood tracking entries
- `memory_board_items` - Memory board entries
- `question_cards` - Game question cards
- `game_rounds` - Game session data
- `goals` - Couple goals
- `anonymous_notes` - Anonymous feedback
- `streaks` - Gamification streaks

## Next Steps

To continue development:

1. **Add More Controllers**:

   - CouplesController
   - LikesController
   - MoodsController
   - MemoryController
   - GamesController

2. **Implement SignalR Hubs**:

   - Create `RelationshipHub` for real-time features
   - Add presence tracking
   - Implement game notifications

3. **Add Validation**:

   - Create FluentValidation validators for DTOs
   - Add input validation middleware

4. **Implement Services**:

   - CoupleService
   - LikesService
   - MoodService
   - GameService
   - AICoachService (stub)

5. **Add Seed Data**:
   - Create sample question cards
   - Add test users and couples

## Troubleshooting

### Cannot connect to PostgreSQL

- Ensure Docker is running: `docker ps`
- Check if port 5432 is available
- Verify connection string in `appsettings.Development.json`

### Migration errors

- Delete the `Migrations` folder and recreate migrations
- Ensure PostgreSQL is running before applying migrations

### JWT authentication not working

- Verify `JwtSettings:SecretKey` is at least 32 characters
- Check token expiration time
- Ensure `Authorization: Bearer <token>` header is set correctly

## License

This project is for educational purposes.

## Contact

For questions or support, please contact the development team.
