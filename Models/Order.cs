using trade_compas.Enums;
using trade_compas.Interfaces.Basic;
using trade_compas.Utilities.DTOs.Order;

namespace trade_compas.Models;

public class Order : IIdentifiable, ITimestampable
{
    public Order(CreateOrderDto dto)
    {
        Id = ++LastId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Status = OrderStatus.New;
        Recipient = dto.Recipient;
        ProductId = dto.ProductId;
        SellerId = dto.SellerId;
    }

    private static int LastId = 0;
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public Recipient Recipient { get; set; }
    public int ProductId { get; set; }
    public string SellerId { get; set; }
}