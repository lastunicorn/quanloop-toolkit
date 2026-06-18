# Quanloop Toolkit

[![GitHub Repo](https://img.shields.io/badge/github-repo-blue?logo=github)](https://github.com/lastunicorn/quanloop-toolkit) [![GitHub Build](https://img.shields.io/github/actions/workflow/status/lastunicorn/quanloop-toolkit/build-master.yml?logo=github)](https://github.com/lastunicorn/quanloop-toolkit/actions/workflows/build-master.yml) [![NuGet Version](https://img.shields.io/nuget/v/DustInTheWind.Quanloop.Toolkit?logo=nuget)](https://www.nuget.org/packages/DustInTheWind.Quanloop.Toolkit) [![NuGet Downloads](https://img.shields.io/nuget/dt/DustInTheWind.Quanloop.Toolkit?logo=nuget)](https://www.nuget.org/packages/DustInTheWind.Quanloop.Toolkit)

`Quanloop Toolkit` is a .NET library for parsing `.csv` files exported from the Quanloop.

Quanloop is a peer-to-peer investment platform.

- https://www.quanloop.com/en

## Installation

Package Manager:

```powershell
Install-Package DustInTheWind.Quanloop.Toolkit
```

.NET CLI:

```bash
dotnet add package DustInTheWind.Quanloop.Toolkit
```

## Runtime Requirements

- Library target framework: `.NET 8.0` (`net8.0`)

## Features

- **Parse Quanloop Statement Documents** - Load and parse CSV files exported directly from the Quanloop platform.

## Quick Start

### a) Export the Transactions CSV File

In Quanloop web application:

1. Log in.
2. Select the **Account statement** item in the top menu.
3. Select the date interval you need and click **Filter**.
4. Download the statement in the CSV format.

You will get a CSV containing transaction rows that can be parsed with this toolkit.

### b) Parse the Exported Document

```csharp
using DustInTheWind.Quanloop.Toolkit;

StatementDocument document = StatementDocument.LoadFromFile("statement.csv");

foreach (TransactionRecord transaction in document)
{
	...
}
```

## CSV Statement Document

Each row is mapped to a `TransactionRecord` with the following columns:

| CSV Column      | Type     | TransactionRecord Property | Description                                         |
|-----------------|----------|--------------------------|-----------------------------------------------------|
| `Date`          | `DateTime` | `Date`                   |              |
| `Sender/Receiver` | `string` | `SenderOrReceiver` |             |
| `Account` | `string` | `Account`         |  |
| `BIC/SWIFT` | `string` | `BicSwift` |                              |
| `Description` | `string` | `Description`     |          |
| `Amount` | `decimal` | `Amount`         |                 |
| `Balance` | `decimal` | `Balance`     |  |

## Demo Project

The repository includes a sample CLI project in `sources/Quanloop.Toolkit.Demo` that demonstrates:

- reading `statement.csv`
- printing parsed data.

You can use this project as a reference implementation for your own importer/exporter tools.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
