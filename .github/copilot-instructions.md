# Copilot Instructions for HeThongDatBan

## Project Overview
- **HeThongDatBan** is an ASP.NET Core MVC application for restaurant reservation management.
- The architecture follows standard MVC patterns: `Controllers/`, `Models/`, `Views/`, and `Services/`.
- Data access is handled via Entity Framework Core, with context and data classes in `Data/` and `Models/`.
- Business logic and API endpoints are in `Controllers/` and `Services/`.
- Static assets are in `wwwroot/` and `Assets/`.

## Key Components
- **Controllers**: Handle HTTP requests, route logic, and interact with services/data.
  - Example: `Controllers/NhaHangController.cs` manages restaurant endpoints.
- **Models**: Define data structures and EF Core entities (e.g., `Models/NhaHang.cs`, `Models/DatBan.cs`).
- **Services**: Contain business logic and background tasks (e.g., `Services/CancelExpiredReservationsService.cs`).
- **Middleware**: Custom authentication/authorization logic in `Controllers/JwtCookieMiddleware.cs` and `Middleware/`.
- **Views**: Razor views in `Views/` (organized by feature).

## Developer Workflows
- **Build**: Use `dotnet build` in the project root or via Visual Studio.
- **Run**: Use `dotnet run` or launch via Visual Studio. The main entry is `Program.cs`.
- **Debug**: Launch profiles are in `Properties/launchSettings.json`.
- **Configuration**: App settings in `appsettings.json` and `appsettings.Development.json`.
- **Static Files**: Place in `wwwroot/` for serving (e.g., images, CSS, JS).

## Project-Specific Patterns
- **Authentication**: JWT-based, with custom middleware for cookie handling (`Controllers/JwtCookieMiddleware.cs`).
- **Data Access**: Use `ApplicationDbContext` (`Data/ApplicationDbContext.cs`) for EF Core operations.
- **API Results**: Standardized via `Models/ApiResult.cs`.
- **Background Tasks**: Implemented as hosted services in `Services/`.
- **View Organization**: Feature-based folders under `Views/` (e.g., `Views/CMS/`, `Views/NhaHang/`).

## Integration Points
- **External Libraries**: Uses Dapper, Humanizer, Azure SDKs, and Microsoft.AspNetCore.Authentication.JwtBearer.
- **Uploads**: User-uploaded files are stored in `wwwroot/Uploads/`.

## Examples
- To add a new feature, create a model in `Models/`, a controller in `Controllers/`, and views in `Views/<Feature>/`.
- For new background jobs, add a service to `Services/` and register it in `Program.cs`.

---

For questions or unclear patterns, review the structure of `Controllers/`, `Models/`, and `Services/` for examples. Ask for feedback if any workflow or pattern is ambiguous.
