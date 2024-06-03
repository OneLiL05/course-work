using System.ComponentModel.DataAnnotations;

namespace trade_compas.DTOs.Product;

public class CreateProductDto
{
    public CreateProductDto() {}

    public CreateProductDto(CreateProductDto dto)
    {
        Name = dto.Name;
        Price = dto.Price;
        State = dto.State;
        CategorySlug = dto.CategorySlug;
    }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(256, ErrorMessage = "Name length can't be more than 256 and less than 5", MinimumLength = 5)]
    public string Name { get; set; }

    [StringLength(500, ErrorMessage = "Description length can't be more than 500")]
    public string? Description { get; set; } = "";

    [Range(1.0, double.MaxValue, ErrorMessage = "Price should be at least $1")]
    public double Price { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string CategorySlug { get; set; }

    public string SellerId { get; set; } = "";

    public string Img { get; set; } = "";
}