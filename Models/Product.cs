using System.Text.Json.Serialization;
using trade_compas.DTOs.Product;
using trade_compas.Interfaces.Basic;

namespace trade_compas.Models;

public class Product : CreateProductDto, IIdentifiable, ITimestampable
{
    public Product() {}

    public Product(CreateProductDto dto)
    {
        Id = ++lastId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Name = dto.Name;
        Description = dto.Description;
        Price = dto.Price;
        State = dto.State;
        CategorySlug = dto.CategorySlug;
        SellerId = dto.SellerId;
    }

    public Product(Product product)
    {
        Id = product.Id;
        CreatedAt = product.CreatedAt;
        UpdatedAt = product.UpdatedAt;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        State = product.State;
        CategorySlug = product.CategorySlug;
        SellerId = product.SellerId;
    }

    private static int lastId = 0;
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool InArchive { get; set; } = false;
}
