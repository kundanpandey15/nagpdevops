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
var userName = _configuration.GetSection("username").Value ?? "";
var password = _configuration.GetSection("password").Value ?? "";
var connectionString = String.Format(_configuration.GetSection("ConnectionString").Value ?? "", userName, password);

//MongoCredential credential = MongoCredential.CreateCredential("admin", userName, password);
//var settings = new MongoClientSettings
//{
//    Credential = credential,
//    Server = new MongoServerAddress(_configuration.GetSection("MONGO_HOST").Value, Convert.ToInt32(_configuration.GetSection("MONGO_PORT").Value))
//};

var mongoClient = new MongoClient(new MongoUrl(connectionString));
var database = mongoClient.GetDatabase("WeatherForecast");

// Register IMongoDatabase for dependency injection
builder.Services.AddSingleton(database);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
