
namespace gameStore.EndPoints;

using gamestore.Data;
using gameStore.Dtos;
using gamestore.Entities;
using gamestore.Mapping;
using Microsoft.EntityFrameworkCore;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        //we define the route with group, important to use RouteGroupBuilder in the method
        var group = app.MapGroup("games")
        //Package for using the validations in CreateGameDto
        .WithParameterValidation();

        //GET all games from the list
        group.MapGet("/",async (GameStoreContext dbContext) => await dbContext.Games
                                                                    .Include(game => game.Genre)        
                                                                    .Select(game => game.ToGameSummaryDto())
                                                                    .AsNoTracking()
                                                                    .ToListAsync());

        //GET games with id

        group.MapGet("/{id}", async(int id,GameStoreContext dbContext) =>
        {
            //GameDto? indicates that game can be a GameDto object or null (nullable type).
            Game? game = await dbContext.Games.FindAsync(id);
            //if game is nul it returns a 404 not found but if is not null returns a 200 OK with the object found
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);


        //POST Create a new game                 We declare the use of the db connection using GameStorecontext dbcontext

        group.MapPost("/", async (CreateGamedto newGame,GameStoreContext dbContext) =>
        {

           //Using the db context to insert into the database
            Game game = newGame.ToEntity();
           
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });
        //Update a game
        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame,GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);   
            //If a game not exists index will return -1
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToEntity(id));           
            await dbContext.SaveChangesAsync();
            return Results.NoContent();

        });

        //Delete a game

        group.MapDelete("/{id}", async (int id,GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();    

            return Results.NoContent();
        });
        return group;
    }
}