using trade_compas.Enums;
using trade_compas.Interfaces.Basic;
using trade_compas.Utilities.DTOs.Order;

namespace trade_compas.Models;

public class Order : CreateOrderDto, IIdentifiable, ITimestampable
{
    public Order() {}

    public Order(CreateOrderDto dto)
    {
        Id = ++LastId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Status = OrderStatus.New;
        Recipient = dto.Recipient;
        Product = dto.Product;
    }

    private static int LastId = 0;
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public OrderStatus Status { get; set; }
}