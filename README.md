# easyEmployerBackend

Backend API for creating employers via the Loket API.

## Features

- RESTful endpoint for employer creation
- Integration with Loket API
- Configurable API settings
- Error handling and logging
- Thread-safe HTTP client implementation

## Quick Start

```bash
# Build the project
dotnet build

# Run the application
dotnet run
```

The API will be available at `http://localhost:5183`

## API Endpoint

**POST /api/employer**

Creates a new employer in the Loket system.

Request:
```json
{
  "accessToken": "your-access-token",
  "werkgevernaam": "Company Name BV"
}
```

Response:
```json
{
  "id": "newly-created-employer-id"
}
```

For detailed documentation, see [API_DOCUMENTATION.md](API_DOCUMENTATION.md)

## Configuration

Configure the Loket API settings in `appsettings.json`:

```json
{
  "LoketApi": {
    "BaseUrl": "https://api.loket-ontw.nl/v2",
    "ProviderId": "3b34f479-7b46-4944-b579-acee7257e0cc"
  }
}
```

## Technology Stack

- ASP.NET Core 10.0
- C# 
- RESTful API design

