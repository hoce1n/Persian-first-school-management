# School Management System

A Persian-first school management system built with **ASP.NET Core Blazor**, **Entity Framework Core**, and **SQL Server**. It gives school administrators and teachers a focused workspace for managing people, classes, attendance, and attendance reports.

## Features

- Persian/Farsi user interface with Persian calendar date formatting
- Cookie-based authentication and role-aware navigation
- Admin seeding for first-time local setup
- Student and teacher management
- Class creation and student enrollment
- Daily attendance tracking with present, absent, and excused statuses
- Attendance reports grouped by date and class
- SMS link shortcuts for notifying parents about absences
- School information settings
- SQL Server persistence through Entity Framework Core migrations
- Responsive styling with Tailwind CSS

## Tech stack

- **.NET 9 / ASP.NET Core**
- **Blazor Web App** with interactive server components
- **Entity Framework Core 9** and SQL Server
- **Tailwind CSS 4**
- **Bun** for frontend tooling

## Getting started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) or SQL Server Express/LocalDB
- [Bun](https://bun.sh/) (recommended for the CSS watch script)

### 1. Clone the repository

```bash
git clone https://github.com/hoce1n/school.git
cd school
```

### 2. Configure the database

The repository intentionally does **not** include a production connection string. Set the connection string through the standard ASP.NET Core environment variable:

```bash
# Linux/macOS
export ConnectionStrings__DefaultConnection='Server=localhost;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True;'

# PowerShell
$env:ConnectionStrings__DefaultConnection = 'Server=localhost;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True;'
```

You can also update `appsettings.Development.json` for a machine-specific local connection. Never commit passwords, tokens, or production connection strings.

### 3. Install frontend dependencies

```bash
bun install
```

### 4. Apply migrations and run

```bash
dotnet ef database update
dotnet run
```

For live .NET and Tailwind CSS development, use:

```bash
bun run dev
```

The application will print its local HTTPS/HTTP URLs when it starts.

## Development configuration

`appsettings.Development.json` contains only safe development defaults. `appsettings.json` is intentionally excluded from the repository. ASP.NET Core configuration values supplied by environment variables override the JSON defaults; the database setting uses the `ConnectionStrings__DefaultConnection` variable shown above.

The current seed routine creates a local administrator account on first startup:

- **Username:** `Manager`
- **Password:** `Admin@123`

Change or remove these development-only credentials before using the application in any shared or production environment.

## Project structure

```text
Components/   Blazor pages, layouts, navigation, and reusable UI
Controller/   HTTP controllers such as authentication
Data/         EF Core DbContext and seed data
Migrations/   EF Core database migrations
Models/       Domain entities and enums
Services/     Authentication, breadcrumbs, and Persian date helpers
wwwroot/      Static assets, fonts, and generated CSS
```

## Database migrations

After changing an entity or the data model, create a migration and apply it:

```bash
dotnet ef migrations add DescribeYourChange
dotnet ef database update
```

## Security notes

- Keep connection strings and credentials outside source control.
- Use HTTPS and secure cookie settings when deploying.
- Replace the development seed password before any non-local deployment.
- Rotate any credential that may previously have been committed to a repository.

## License

No license has been specified yet. Add a license before distributing the project publicly.
