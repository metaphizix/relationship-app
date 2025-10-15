# 🚀 Quick Start Guide - Relationship App

Get your Relationship App backend up and running in 5 minutes!

## Prerequisites Check

Before starting, ensure you have:

- ✅ [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed
- ✅ [Docker Desktop](https://www.docker.com/products/docker-desktop) running
- ✅ A code editor (VS Code, Visual Studio, or Rider)

Verify installations:

```powershell
dotnet --version  # Should show 9.x.x
docker --version  # Should show Docker version
```

---

## Step 1: Start PostgreSQL Database

Open PowerShell and run:

```powershell
cd "c:\Developer\Mobile App\backend"
docker-compose up -d
```

**Expected output:**

```
Creating network "backend_default" with the default driver
Creating volume "backend_postgres_data" with default driver
Creating relationshipapp_postgres ... done
Creating relationshipapp_pgadmin  ... done
```

**Verify it's running:**

```powershell
docker ps
```

You should see two containers running: `relationshipapp_postgres` and `relationshipapp_pgadmin`.

---

## Step 2: Apply Database Migrations

The migration files are already created, now apply them:

```powershell
# Still in c:\Developer\Mobile App\backend
dotnet ef database update -p RelationshipApp.Infrastructure -s RelationshipApp.Api
```

**Expected output:**

```
Build started...
Build succeeded.
Applying migration '20251015_InitialCreate'.
Done.
```

---

## Step 3: Run the API

```powershell
cd RelationshipApp.Api
dotnet run
```

**Expected output:**

```
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
```

**🎉 Your API is now running!**

---

## Step 4: Test the API

### Option A: Use Swagger UI (Recommended)

1. Open your browser
2. Navigate to: **https://localhost:5001/swagger**
3. You'll see the interactive API documentation

### Option B: Test with cURL

Open a **new PowerShell window** and test the health endpoint:

```powershell
curl http://localhost:5000/
```

**Expected response:**

```json
{ "message": "Relationship App API is running!", "version": "1.0.0" }
```

---

## Step 5: Create Your First User

### Using Swagger UI:

1. Go to https://localhost:5001/swagger
2. Find `POST /api/auth/register`
3. Click "Try it out"
4. Replace the example with:

```json
{
  "email": "alice@example.com",
  "password": "SecurePassword123!",
  "displayName": "Alice"
}
```

5. Click "Execute"
6. Copy the `token` from the response

### Using cURL:

```powershell
$body = @{
    email = "alice@example.com"
    password = "SecurePassword123!"
    displayName = "Alice"
} | ConvertTo-Json

$response = Invoke-RestMethod -Uri "http://localhost:5000/api/auth/register" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"

$response
```

**Save your token!** You'll need it for authenticated requests.

---

## Step 6: Create a Couple

### Using Swagger UI:

1. Click the "Authorize" button at the top
2. Enter: `Bearer YOUR_TOKEN_HERE` (replace with your actual token)
3. Click "Authorize"
4. Find `POST /api/couples`
5. Click "Try it out"
6. Use this body:

```json
{}
```

7. Click "Execute"
8. **Save the `code`** from the response - you'll need this for the second partner to join!

### Using cURL:

```powershell
$token = "YOUR_TOKEN_HERE"  # Replace with your actual token

$headers = @{
    "Authorization" = "Bearer $token"
    "Content-Type" = "application/json"
}

$couple = Invoke-RestMethod -Uri "http://localhost:5000/api/couples" `
    -Method Post `
    -Headers $headers `
    -Body "{}"

$couple
Write-Host "`nCouple Code: $($couple.code)" -ForegroundColor Green
```

---

## Step 7: Add a Second Partner

1. **Register a second user** (repeat Step 5 with different email)
2. **Use the join endpoint** with the couple code:

### Using Swagger UI:

1. Authorize with the second user's token
2. Find `POST /api/couples/{id}/join`
3. Enter any GUID for the `id` (it's not used in the current implementation)
4. Use this body:

```json
{
  "code": "ABC123"
}
```

(Replace ABC123 with your actual couple code)

### Using cURL:

```powershell
$token2 = "SECOND_USER_TOKEN"  # Replace with second user's token
$coupleCode = "ABC123"  # Replace with your couple code

$headers = @{
    "Authorization" = "Bearer $token2"
    "Content-Type" = "application/json"
}

$body = @{
    code = $coupleCode
} | ConvertTo-Json

$result = Invoke-RestMethod -Uri "http://localhost:5000/api/couples/00000000-0000-0000-0000-000000000000/join" `
    -Method Post `
    -Headers $headers `
    -Body $body

$result
```

---

## 🎉 Success! What's Working

You now have:

✅ PostgreSQL database running in Docker  
✅ ASP.NET Core API running on ports 5000 (HTTP) and 5001 (HTTPS)  
✅ User registration and authentication with JWT  
✅ Couple creation and joining with unique invite codes  
✅ Full API documentation via Swagger UI

---

## 🔧 Useful Commands

### Database Management

```powershell
# View database with pgAdmin
# Go to: http://localhost:5050
# Login: admin@relationshipapp.com / admin

# Connect to PostgreSQL directly
docker exec -it relationshipapp_postgres psql -U postgres -d relationshipapp_dev

# View tables
\dt

# Query users
SELECT * FROM users;

# Exit
\q
```

### API Management

```powershell
# Stop the API
# Press Ctrl+C in the terminal running dotnet run

# View logs
# Logs are saved in: backend\RelationshipApp.Api\logs\

# Rebuild
dotnet build

# Clean and rebuild
dotnet clean
dotnet build
```

### Docker Management

```powershell
# Stop all containers
docker-compose down

# Stop and remove data
docker-compose down -v

# View logs
docker logs relationshipapp_postgres
docker logs relationshipapp_pgadmin

# Restart containers
docker-compose restart
```

---

## 🐛 Troubleshooting

### "Port 5432 is already in use"

Another PostgreSQL instance is running:

```powershell
# Windows: Stop PostgreSQL service
Stop-Service postgresql-x64-*

# Or change the port in docker-compose.yml:
ports:
  - "5433:5432"  # Use port 5433 instead

# Update appsettings.Development.json:
"Host=localhost;Port=5433;..."
```

### "Cannot connect to PostgreSQL"

```powershell
# Check if container is running
docker ps | Select-String postgres

# If not running, start it
docker-compose up -d postgres

# Check logs for errors
docker logs relationshipapp_postgres
```

### "Migration already applied" error

```powershell
# Drop the database and recreate
dotnet ef database drop -p RelationshipApp.Infrastructure -s RelationshipApp.Api --force
dotnet ef database update -p RelationshipApp.Infrastructure -s RelationshipApp.Api
```

### "JWT Secret Key" error

The secret key in `appsettings.json` must be at least 32 characters. It's already configured correctly, but if you change it, ensure it meets this requirement.

---

## 📊 Test Data Quick Setup

Want some test data? Run these in order:

```powershell
# 1. Register Alice
$alice = @{
    email = "alice@example.com"
    password = "Password123!"
    displayName = "Alice"
} | ConvertTo-Json |
Invoke-RestMethod -Uri "http://localhost:5000/api/auth/register" -Method Post -ContentType "application/json"

# 2. Register Bob
$bob = @{
    email = "bob@example.com"
    password = "Password123!"
    displayName = "Bob"
} | ConvertTo-Json |
Invoke-RestMethod -Uri "http://localhost:5000/api/auth/register" -Method Post -ContentType "application/json"

# 3. Alice creates a couple
$coupleResponse = Invoke-RestMethod -Uri "http://localhost:5000/api/couples" `
    -Method Post `
    -Headers @{ "Authorization" = "Bearer $($alice.token)" } `
    -ContentType "application/json" `
    -Body "{}"

Write-Host "Couple Code: $($coupleResponse.code)" -ForegroundColor Green

# 4. Bob joins with the code
$joinBody = @{ code = $coupleResponse.code } | ConvertTo-Json
Invoke-RestMethod -Uri "http://localhost:5000/api/couples/00000000-0000-0000-0000-000000000000/join" `
    -Method Post `
    -Headers @{ "Authorization" = "Bearer $($bob.token)" } `
    -ContentType "application/json" `
    -Body $joinBody

Write-Host "✅ Test couple created successfully!" -ForegroundColor Green
```

---

## 🎯 Next Steps

Now that your backend is running, you can:

1. **Explore the API** - Use Swagger UI to test all endpoints
2. **View the database** - Open pgAdmin at http://localhost:5050
3. **Read PROJECT_SUMMARY.md** - Understand what's implemented and what's next
4. **Start building features** - Follow the implementation plan in PROJECT_SUMMARY.md
5. **Create the mobile app** - Set up React Native frontend

---

## 📚 Important URLs

| Service     | URL                            | Credentials                       |
| ----------- | ------------------------------ | --------------------------------- |
| API (HTTP)  | http://localhost:5000          | -                                 |
| API (HTTPS) | https://localhost:5001         | -                                 |
| Swagger UI  | https://localhost:5001/swagger | -                                 |
| pgAdmin     | http://localhost:5050          | admin@relationshipapp.com / admin |
| PostgreSQL  | localhost:5432                 | postgres / postgres               |

---

## ✅ You're All Set!

Your Relationship App backend is now running and ready for development. Check out the **PROJECT_SUMMARY.md** file for the complete implementation roadmap.

**Happy coding! 🚀❤️**
