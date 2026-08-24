# ECommerce Monorepo Scaffold (.NET 8)

This repository contains a production-oriented initial scaffold for an e-commerce platform using:

- **Backend:** ASP.NET Core Web API (.NET 8)
- **Frontend:** Blazor WebAssembly standalone (.NET 8)
- **Shared contracts:** Class library for DTOs/enums/common responses
- **Database:** EF Core with PostgreSQL provider (SQL Server compatible design intent)
- **Authentication:** ASP.NET Core Identity + JWT
- **Testing:** xUnit baseline project

## Architecture overview

- `src/ECommerce.API` - API, Identity, JWT auth, EF Core model, middleware, seeders
- `src/ECommerce.Client` - Blazor WASM client shell (static-host compatible)
- `src/ECommerce.Shared` - Shared enums/DTOs/contracts
- `tests/ECommerce.Tests` - Initial unit tests

## Solution structure

```text
ECommerce.sln
src/
  ECommerce.API/
  ECommerce.Client/
  ECommerce.Shared/
tests/
  ECommerce.Tests/
```

## Prerequisites

- .NET SDK 8.0+
- PostgreSQL 15+ (or compatible)

## Local setup

1. Restore:
   ```bash
   dotnet restore /home/runner/work/ECommerce/ECommerce/ECommerce.sln
   ```
2. Configure API settings:
   - `/home/runner/work/ECommerce/ECommerce/src/ECommerce.API/appsettings.Development.json`
   - Use environment variables for secrets:
     - `ConnectionStrings__DefaultConnection`
     - `Jwt__Secret`
     - `Seed__AdminPassword`

## Database migrations

Migration-ready model is implemented. Generate/update migrations locally with:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate --project /home/runner/work/ECommerce/ECommerce/src/ECommerce.API/ECommerce.API.csproj --startup-project /home/runner/work/ECommerce/ECommerce/src/ECommerce.API/ECommerce.API.csproj --output-dir Data/Migrations
dotnet ef database update --project /home/runner/work/ECommerce/ECommerce/src/ECommerce.API/ECommerce.API.csproj --startup-project /home/runner/work/ECommerce/ECommerce/src/ECommerce.API/ECommerce.API.csproj
```

## Running API

```bash
dotnet run --project /home/runner/work/ECommerce/ECommerce/src/ECommerce.API/ECommerce.API.csproj
```

API includes:
- Identity + JWT wiring
- Auth endpoints:
  - `POST /api/auth/register`
  - `POST /api/auth/login`
  - `POST /api/auth/refresh` (placeholder)
  - `POST /api/auth/forgot-password` (placeholder)
- Swagger with ****** support
- Global exception middleware returning ProblemDetails-style responses

## Running Client

```bash
dotnet run --project /home/runner/work/ECommerce/ECommerce/src/ECommerce.Client/ECommerce.Client.csproj
```

Client shell includes routes for:
- Home
- Products
- ProductDetails
- Cart
- Checkout
- Login
- Register
- Profile
- Wishlist
- Orders

## GitHub Pages deployment (frontend only)

Workflow: `/home/runner/work/ECommerce/ECommerce/.github/workflows/deploy.yml`

- Builds/publishes **Blazor WASM** only
- Deploys static `wwwroot` to GitHub Pages
- Sets `base href` to `/ECommerce/`
- Copies `index.html` to `404.html` for SPA route fallback

> GitHub Pages hosts the Blazor client only. The API/database must be hosted separately.

## Backend hosting note

Deploy API separately to ASP.NET-compatible hosting (Azure App Service, Render, Railway, etc.).
Set CORS `AllowedOrigins` to the production client URL.

## Seed data

Startup seeding includes:
- Roles: `Admin`, `Customer`
- Admin user: `admin@example.com` (created only if `Seed:AdminPassword` is provided)
- ~5 categories and ~20 sample products

## Security checklist

- Do **not** commit real secrets
- Use env vars/secret stores for JWT secret, DB credentials, admin password
- Passwords are handled via ASP.NET Core Identity hashing
- No payment card storage included (payment abstraction only)
