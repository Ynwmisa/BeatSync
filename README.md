# BeatSync

BeatSync is an ASP.NET Core web application for managing and selling beats.

## Quick start

Requirements:
- .NET 9 SDK (or the version used in this repo)
- (Optional) SQLite if using the included local DB

Run locally:
```powershell
dotnet build
dotnet run --project BeatSync.Web
```

Database / migrations:
- Migrations live under `Migrations/`.
- To update DB (example):
```powershell
dotnet ef database update --project BeatSync.Web
```

Development notes:
- App settings: `appsettings.json`, `appsettings.Development.json`
- Identity pages: `Areas/Identity/Pages/`

License:
- Add license information here (e.g., MIT)

Contributing:
- Use feature branches and open a pull request when ready.
