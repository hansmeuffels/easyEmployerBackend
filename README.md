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

## Deployment

This is a .NET Core backend application. It is **not compatible with Cloudflare Workers** without additional tooling.

### Suitable Hosting Platforms

- **Azure App Service** (recommended for .NET)
- **AWS Elastic Beanstalk** or **AWS Lambda** (with .NET runtime)
- **Google Cloud Run**
- **Traditional hosting** (VPS, dedicated server)
- **Docker containers** (any container platform)

### Cloudflare Workers Note

A `wrangler.jsonc` configuration file exists in this repository to resolve deployment errors, but the application cannot run on Cloudflare Workers natively. Cloudflare Workers is designed for JavaScript/TypeScript applications.

If you need to use Cloudflare infrastructure, consider:
1. Hosting the .NET backend on a traditional platform
2. Using Cloudflare as a CDN/proxy in front of your backend
3. Rewriting the backend in JavaScript/TypeScript for native Cloudflare Workers support

