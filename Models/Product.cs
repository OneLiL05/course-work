using trade_compas.DTOs.Product;
using trade_compas.Interfaces.Basic;

namespace trade_compas.Models;

public class Product : CreateProductDto, IIdentifiable, ITimestampable, IReviewable, IArchivable
{
    public Product() {}

    public Product(CreateProductDto dto)
    {
        Id = ++_lastId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Name = dto.Name;
        Description = dto.Description;
        Price = dto.Price;
        State = dto.State;
        CategorySlug = dto.CategorySlug;
        SellerId = dto.SellerId;
        Img = dto.Img;
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
        Img = product.Img;
    }

    private static int _lastId;
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Review> Reviews { get; set; } = [];
    public int Ranking { get; set; }
    public bool IsArchived { get; set; }
}
