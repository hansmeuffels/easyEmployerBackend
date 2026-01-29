# Easy Employer Backend API

## Overview
This is a backend API that creates employers via the Loket API.

## Endpoint

### POST /api/employer
Creates a new employer in the Loket system.

**Request Body:**
```json
{
  "accessToken": "your-access-token",
  "werkgevernaam": "Employer Name BV"
}
```

**Response (Success - 200 OK):**
```json
{
  "id": "newly-created-employer-id"
}
```

**Error Responses:**
- `400 Bad Request` - Missing or invalid input parameters
- `502 Bad Gateway` - Failed to communicate with Loket API
- `500 Internal Server Error` - Unexpected error occurred

## Configuration

The application can be configured via `appsettings.json`:

```json
{
  "LoketApi": {
    "BaseUrl": "https://api.loket-ontw.nl/v2",
    "ProviderId": "3b34f479-7b46-4944-b579-acee7257e0cc"
  }
}
```

Environment-specific configuration can be added in `appsettings.Development.json` or `appsettings.Production.json`.

## Running the Application

### Prerequisites
- .NET 10.0 SDK or later

### Build
```bash
dotnet build
```

### Run
```bash
dotnet run
```

The API will be available at `http://localhost:5183` (or the configured port).

## Testing

You can test the endpoint using curl:

```bash
curl -X POST http://localhost:5183/api/employer \
  -H "Content-Type: application/json" \
  -d '{
    "accessToken": "your-access-token",
    "werkgevernaam": "Test Company BV"
  }'
```

Or use the provided `.http` file with REST Client extensions in VS Code.

## Architecture

- **Controllers/EmployerController.cs**: Handles HTTP requests and responses
- **Services/LoketService.cs**: Manages communication with the Loket API
- **Models/**: Contains request/response DTOs
- **Program.cs**: Application configuration and dependency injection setup

## Security Notes

- Access tokens are passed through to the Loket API but are not validated by this service
- Error details are not exposed to clients to prevent information leakage
- HTTP timeouts are configured to prevent hanging requests
- Consider adding authentication/authorization middleware for production use
