# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What This Is

`Quanloop Toolkit` is a .NET library (`DustInTheWind.Quanloop.Toolkit`) for parsing CSV statement files exported from the [Quanloop](https://www.quanloop.com/en) peer-to-peer investment platform. It is published as a NuGet package.

## Commands

```bash
# Restore packages
dotnet restore ./Quanloop.Toolkit.slnx --configfile ./nuget.config

# Build
dotnet build ./Quanloop.Toolkit.slnx

# Build release (also generates NuGet package)
dotnet build ./Quanloop.Toolkit.slnx -c Release

# Run the demo (reads statement.csv and prints a table)
dotnet run --project sources/Quanloop.Toolkit.Demo
```

There are no test projects yet. The CI workflow (`build-master.yml`) restores and builds in Release mode on push to `master`, requiring both .NET 8 and .NET 10 SDKs.

## Architecture

The library has one main public surface and a thin internal CSV layer:

- **`StatementDocument`** — extends `Collection<TransactionRecord>`. Exposes only static `LoadAsync` factory methods accepting `string filePath`, `string csv`, `Stream`, `FileInfo`, `StreamReader`, or `TextReader`. All exceptions are wrapped in `DocumentLoadException`.
- **`TransactionRecord`** — a `record class` with properties: `Date` (`DateOnly`), `Counterpart`, `Account`, `BicSwift`, `Description`, `Amount` (`decimal`), `Balance` (`decimal`).
- **`DocumentLoadException`** — the single exception type callers need to handle.
- **`Csv/CsvStatementDocument`** (internal) — wraps `CsvHelper.CsvReader`; exposes `IAsyncEnumerable<TransactionRecord>` via `ReadTransactions`.
- **`Csv/TransactionRecordMap`** (internal) — `ClassMap<TransactionRecord>` mapping CSV column names (`Sender/Receiver` → `Counterpart`, etc.) to record properties.

Assembly names are always prefixed `DustInTheWind.` (set via `AssemblyName` in each `.csproj`). Shared metadata (version, copyright, package tags) lives in `Directory.Build.props`.

The Demo project (`net10.0`) references the library directly and uses `ConsoleTools.Controls.Tables` to render a `DataGrid`.

## Code Conventions

From `.github/copilot-instructions.md`:

- Do not use `var`; always write the actual type.
- In LINQ lambdas, name the item parameter `x`.
- Prefer `new()` (target-typed) over `new T()` when instantiating objects.
- When using object initializer syntax with more than one property, put each property on its own line.
- Omit curly braces on `if`, `for`, and `using` when the body is a single line.
- XML documentation is only for public types that end up in the NuGet package; skip it for internal types.

## Test Conventions

- One test file per public method (including constructors): e.g., a method `Query()` → file `QueryTests.cs`.
- Group all test files for a class in a directory named `<ClassName>Tests/`.
- Test method naming: `Having<Setup>_When<Action>_Then<Expected>`.
- In `Assert.Throws` calls, always use a block body (`{ }`) for the lambda, not an expression body.
