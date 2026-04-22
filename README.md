# ComputerGamesORM

Production-grade three-layer console application with Entity Framework Core (Code First) for managing `Games` in database `ComputerGamesORM`.

## Project structure

- `src/ComputerGamesORM.Data` — Data layer
  - `Models/Game.cs` — entity model for table `Games`
  - `ComputerGamesContext.cs` — EF Core `DbContext`
  - `Configurations/GameConfiguration.cs` — fluent mapping configuration
- `src/ComputerGamesORM.Business` — Business layer
  - `IGameService.cs` — service contract
  - `GameService.cs` — business logic, CRUD operations, validation
  - `GameDto.cs` — data transfer model for presentation
- `src/ComputerGamesORM.Presentation` — Presentation layer
  - `ConsoleUi.cs` — console menu and user interaction
  - `Program.cs` — app entry point, EF context initialization

## Features

1. List all games (ID + Name)
2. Add new game
3. Update game by ID
4. Fetch game name by ID
5. Delete game by ID

## Rules implemented

- Update by non-existing ID prints exactly: `Game not found!`
- Fetch by non-existing/out-of-range ID performs no output and no extra action
- Successful delete prints exactly: `Done.`
- User input is validated
- Empty/invalid names are blocked by business-level validation

## Build and run

```bash
dotnet restore
dotnet build
dotnet run --project src/ComputerGamesORM.Presentation
```

> The application uses SQLite file database `ComputerGamesORM.db` and creates it automatically at startup.
