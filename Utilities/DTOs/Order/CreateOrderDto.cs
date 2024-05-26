using trade_compas.Models;

namespace trade_compas.Utilities.DTOs.Order;

public class CreateOrderDto
{
    public Recipient Recipient { get; set; }
    public int ProductId { get; set; }
    public string SellerId { get; set; } = "";
}