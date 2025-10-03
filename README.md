# BeatSync

[![Build Status](https://github.com/Ynwmisa/BeatSync/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Ynwmisa/BeatSync/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

BeatSync is an ASP.NET Core web application for managing and selling beats. This repository contains the web project (`BeatSync.Web`) plus the data and migration files.

## Quick start

Requirements
- .NET 9 SDK (or the version used in this repo)
- (Optional) SQLite if using the included local DB

Run locally
```powershell
dotnet build
dotnet run --project BeatSync.Web
```

Database / migrations
- Migrations live under `Migrations/`.
- To update DB (example):
```powershell
dotnet ef database update --project BeatSync.Web
```

## Development notes
- App settings: `appsettings.json`, `appsettings.Development.json`
- Identity pages: `Areas/Identity/Pages/`

## Continuous Integration

This repo includes a GitHub Actions workflow that builds the solution and runs any tests on push. See `.github/workflows/dotnet.yml`.

## License

This project is licensed under the MIT License — see the `LICENSE` file for details.

## Contributing

- Use feature branches and open a pull request when ready.
- Keep commits small and descriptive.
