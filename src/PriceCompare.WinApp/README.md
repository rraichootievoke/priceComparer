# PriceCompareApp

A Windows Forms application for comparing and managing price quotes from Oracle iStore and SQL Server databases.

## Features

- **Order Management**: View and filter recent orders
- **Quote Comparison**: Compare MyDoor pricing with iStore quotes
- **Data Import**: Fetch quote data from Oracle iStore
- **Real-time Updates**: Async operations for responsive UI

## Architecture

This application follows modern design patterns:

- **Repository Pattern**: Data access abstraction
- **Service Layer**: Business logic separation
- **Async/Await**: Non-blocking operations
- **Dapper ORM**: High-performance data access
- **Configuration Management**: Externalized settings

## Quick Start

1. **Configure Connection Strings** in `App.config`
2. **Update Queries** in `queries.json` if needed
3. **Run the application**

## Project Structure

```
PriceCompareApp/
├── Forms/ # UI layer (Form1, Form2)
├── Repositories/ # Data access layer
├── Services/ # Business logic layer
├── Models/           # Data models
├── App.config    # Connection strings
└── queries.json # SQL queries and stored procedures
```

## Technologies

- .NET 8.0 Windows Forms
- Dapper (Micro ORM)
- SQL Server
- Oracle Database
- System.Configuration.ConfigurationManager

## Documentation

For detailed documentation about the refactoring and architecture, see [REFACTORING_README.md](REFACTORING_README.md)

## Recent Updates

- ✅ Implemented Repository Pattern
- ✅ Added Service Layer
- ✅ Converted all operations to async/await
- ✅ Integrated Dapper ORM
- ✅ Externalized configuration
- ✅ Improved error handling
- ✅ Enhanced user experience with wait cursors and feedback

## License

Internal Use Only