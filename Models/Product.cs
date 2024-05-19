using trade_compas.DTOs.Product;
using trade_compas.Interfaces.Basic;

namespace trade_compas.Models;

public class Product : CreateProductDto, IIdentifiable, ITimestampable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool InArchive { get; set; } = false;
    public string SellerId { get; set; }
}
