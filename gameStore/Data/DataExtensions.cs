using Microsoft.EntityFrameworkCore;

namespace gamestore.Data;

public static class DataExtensions{
    public static async Task MigrateDbAsync(this WebApplication app){
        //Execute migration in startup to update Db
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext> ();
        await dbContext.Database.MigrateAsync();
    }
}