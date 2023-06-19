using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddUserSecrets<Program>(true)
    .Build();

// Configure MongoDB
var connectionString = _configuration.GetConnectionString("MongoDB");
var mongoClient = new MongoClient(connectionString);
var databaseName = _configuration.GetValue<string>("MongoDB:DatabaseName");
var database = mongoClient.GetDatabase(databaseName);

// Register IMongoDatabase for dependency injection
builder.Services.AddSingleton(database);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
