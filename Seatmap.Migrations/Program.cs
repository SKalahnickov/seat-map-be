using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Seatmap.Migrations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMigrationContext(builder.Configuration);
var app = builder.Build();
//app.Run();
