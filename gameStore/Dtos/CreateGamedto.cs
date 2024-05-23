using System.ComponentModel.DataAnnotations;

namespace gameStore.Dtos;

public record class CreateGamedto(
    //Validations using DataAnnotations
    [Required][StringLength(50)]string Name,
    int GenreId,
    [Range(0,100)]decimal Price,
    DateOnly ReleaseDate
    );