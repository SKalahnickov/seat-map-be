using Microsoft.EntityFrameworkCore;
using Seatmap.DAL;
using Seatmap.Migrations;
using Seatmap.Models.Settings;
using Seatmap.Services;
using Seatmap.Services.Clients;
using Seatmap.Services.Interfaces;
using Seatmap.Services.Mappers;
using Seatmap.Utils;
using System.Net.Http;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
const string corsConfigName = "ConfiguredOrigin";

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseFilter>();
});
builder.Services.AddCors(options => options.AddPolicy(corsConfigName, builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSeatmapContext(builder.Configuration);
builder.Services.AddMigrationContext(builder.Configuration);
builder.Services.AddSeatmapServices();
builder.Services.AddAutoMapper(typeof(SeatmapEditingProfile));
builder.Services.AddAutoMapper(typeof(SeatmapSelectionProfile));
builder.Services.Configure<IntegrationStrings>(builder.Configuration.GetSection(nameof(IntegrationStrings)));
builder.Services.AddScoped<ITokenHolder, TokenHolder>();
builder.Services.AddScoped<IExternalIntegrationClient, ExternalIntegrationClient>();
builder.Services.AddScoped<HttpClient>(services =>
{
    var httpClient = new HttpClient();
    var tokenHolder = services.GetRequiredService<ITokenHolder>();
    if (tokenHolder.AuthToken != null)
        httpClient.DefaultRequestHeaders.Add("Authorization", tokenHolder.AuthToken);
    return httpClient;
});

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TokenHandlingMiddleware>();

app.UseCors(corsConfigName);
app.Services.MigrateDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
