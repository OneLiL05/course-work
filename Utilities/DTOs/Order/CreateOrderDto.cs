using trade_compas.Models;

namespace trade_compas.Utilities.DTOs.Order;

public class CreateOrderDto
{
    public Recipient Recipient { get; set; }
    public Product Product { get; set; }
}