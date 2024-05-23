using gamestore.Dtos;
using gamestore.Entities;
namespace gamestore.Mapping;

public static class GenreMapping{

    public static GenreDto ToDto(this Genre genre){
        return new GenreDto(genre.Id, genre.Name);
    }

}