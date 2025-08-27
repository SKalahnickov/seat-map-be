# Seat Map Backend

A clean seat map management backend solution.

## Environment Variables

The application requires the following environment variables to be set:

### Database Configuration
- `DB_HOST` - Database server hostname
- `DB_PORT` - Database server port (default: 5432)
- `DB_NAME` - Database name
- `DB_USER` - Database username
- `DB_PASSWORD` - Database password

### External API Configuration
- `EXTERNAL_API_BASE_URL` - Base URL for external API integration

## Example Configuration

Create a `.env` file or set environment variables:

```bash
DB_HOST=localhost
DB_PORT=5432
DB_NAME=seatmap
DB_USER=postgres
DB_PASSWORD=your_password
EXTERNAL_API_BASE_URL=http://localhost:8080
```

## Project Structure

- `Seatmap/` - Main API project
- `Seatmap.Services/` - Business logic and external integrations
- `Seatmap.DAL/` - Data access layer
- `Seatmap.Models/` - Shared models and DTOs
- `Seatmap.Common/` - Common utilities
- `Seatmap.Migrations/` - Database migrations

## Running the Application

1. Set the required environment variables
2. Update database connection string if needed
3. Run database migrations
4. Start the application

```bash
dotnet run --project Seatmap
```

## External Integrations

The application includes sample external data clients that can be replaced with actual implementations:

- `SampleExternalDataClient` - Sample data provider
- `ExternalIntegrationClient` - External API integration client
