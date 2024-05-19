using trade_compas.Enums;
using trade_compas.Interfaces.Basic;

namespace trade_compas.Interfaces.Models;

public interface IProduct : IIdentifiable, ITimestampable
{
    string Name { get; set; }
    string? Description { get; set; }
    double Price { get; set; }
    string Img { get; set; }
    ProductState State { get; set; }
    bool IsArchived { get; set; }
    string CategorySlug { get; set; }
}
