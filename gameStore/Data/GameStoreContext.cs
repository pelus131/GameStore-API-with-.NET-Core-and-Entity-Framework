using gamestore.Entities;
using Microsoft.EntityFrameworkCore;

namespace gamestore.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options){
    public DbSet<Game>Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();
    
    //Seed data to Genre table
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData( 
            new {Id = 1, Name = "Fighting"},
            new {Id = 2, Name = "Roleplayging"},
            new {Id = 3, Name = "Sports"},
            new {Id = 4, Name = "Racing"},
            new {Id = 5, Name = "Kids and Family"}

        );
    }
}