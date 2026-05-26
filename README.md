# ComputerGamesORM

Three-layer .NET 8 Windows Forms desktop application with Entity Framework Core Code First and SQL Server LocalDB.

## Solution Architecture

- `ComputerGamesORM.WinForms` - primary presentation layer. Contains the polished WinForms CRUD UI, DI startup, LocalDB configuration, validation feedback, confirmation dialogs, and async event handlers.
- `ComputerGamesORM.Business` - business logic layer. Contains service contracts, DTOs, edit models, validation rules, and CRUD operations.
- `ComputerGamesORM.Data` - data access layer. Contains EF Core entities, configurations, DbContext, repository implementation, migrations, design-time context factory, and database initializer.
- `ComputerGamesORM.Presentation` - legacy console presentation kept for reference and compatibility.
- `ComputerGamesORM.Tests` - NUnit tests for service-level CRUD and validation behavior using SQLite in-memory relational storage.

## Database Model

Code First creates database `ComputerGamesORM` with two related tables:

- `Games`
  - `Id` primary key
  - `Name` required, max length 200, unique index
- `GameDescriptions`
  - `Id` primary key
  - `Description` required, max length 2000
  - `GameId` required foreign key to `Games.Id`

The relationship is one-to-one. Deleting a game cascades to its description.

## NuGet Packages

Main packages used:

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.Extensions.Configuration.Json`
- `Microsoft.Extensions.DependencyInjection`
- `Microsoft.Extensions.Logging.Abstractions`
- `Microsoft.EntityFrameworkCore.Sqlite` for relational tests
- `NUnit`, `NUnit3TestAdapter`, `Microsoft.NET.Test.Sdk`

## WinForms Features

- Modern dark UI with responsive table/details layout
- Search by ID, game name, or description
- Customized `DataGridView`
- Details/edit panel
- Add, edit, delete, save, cancel, refresh workflows
- Input validation with `ErrorProvider`
- Duplicate name prevention in BLL
- Delete confirmation dialog
- Status notifications
- Startup database migration and sample seed data

## Build, Test, and Run

Restore dependencies:

```bash
dotnet restore
```

Restore EF Core CLI tool:

```bash
dotnet tool restore
```

Build and test:

```bash
dotnet build
dotnet test
```

Run the Windows Forms app:

```bash
dotnet run --project src/ComputerGamesORM.WinForms
```

The WinForms app uses LocalDB by default:

```text
Server=(localdb)\MSSQLLocalDB;Database=ComputerGamesORM;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true
```

The database is migrated automatically on startup.

The Docker target is intentionally limited to the legacy console project. The production WinForms UI is an interactive Windows desktop application and should be launched on Windows.

## EF Core Migration Commands

Add a migration:

```bash
dotnet ef migrations add InitialCreate --project src/ComputerGamesORM.Data --startup-project src/ComputerGamesORM.WinForms --context ComputerGamesContext --output-dir Migrations
```

Apply migrations manually:

```bash
dotnet ef database update --project src/ComputerGamesORM.Data --startup-project src/ComputerGamesORM.WinForms --context ComputerGamesContext
```

## Future Improvements

- Add pagination for very large game catalogs.
- Add richer metadata such as genre, platform, release year, and publisher.
- Add integration tests against SQL Server LocalDB in addition to the SQLite relational tests.
- Add import/export for CSV or Excel catalog management.
