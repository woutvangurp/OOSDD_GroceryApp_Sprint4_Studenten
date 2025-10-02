# GroceryApp

Cross-platform (.NET MAUI, .NET 8, C# 12) grocery list application using MVVM (CommunityToolkit.Mvvm).

## Projects
- Grocery.App: MAUI UI (Shell navigation, views, viewmodels)
- Grocery.Core: Domain models, helpers (e.g. password hashing)
- Grocery.Core.Data: In-memory repositories (e.g. GroceryListRepository)
- TestCore: Unit tests (password verification)

## Key Features
- Role-based login (admin vs user)
- Grocery lists and list items
- Navigation via `AppShell` registered routes
- Admin-only tab visibility (`BoughtProductsTab`)

## Build & Run
Install workloads:

Grocery.App/AppShell.xaml.cs

Build:
dotnet build

Run (exapmle):
dotnet build -t:Run -f net8.0-android

##Tests

dotnet tests

Some negative password tests need implementation.

## Repository Notes
`GroceryListRepository.Add` / `Delete` not implemented; `Update` does not persist into the list (needs index replacement).

## Next Steps (Suggested)
Implement full CRUD, complete tests, persist data (e.g. API/SQLite), harden role handling.