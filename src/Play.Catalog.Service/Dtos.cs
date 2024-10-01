using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service.Dtos
{
    public record ItemDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        DateTimeOffset CreatedDate
        );

    public record CreateItemDto(
        [Required] string Name, // This is the required field

        string Description,
        [Range(0, 1000)] decimal Price // Price must be between 0 and 1000

        );

    public record UpdateItemDto(
        [Required] string Name,
        string Description,
        [Range(0, 1000)] decimal Price
    );
}