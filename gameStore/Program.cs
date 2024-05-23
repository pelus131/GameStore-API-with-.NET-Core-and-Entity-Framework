using gamestore.Data;
using gameStore.EndPoints;
using gamstore.Endpoints;

var builder = WebApplication.CreateBuilder(args);
//Use the connection in appsettings.json
var connString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();


//Use the endpoints in app
app.MapGamesEndpoints();
app.MapGenresEndpoints();
//Update and create database every startup
await app.MigrateDbAsync();

app.Run();
