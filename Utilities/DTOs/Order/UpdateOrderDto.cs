using trade_compas.Enums;

namespace trade_compas.Utilities.DTOs.Order;

public class UpdateOrderDto
{
    public UpdateOrderDto() {}

    public UpdateOrderDto(OrderStatus status)
    {
        Status = status;
    }

    public OrderStatus Status { get; set; }
}