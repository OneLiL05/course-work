using trade_compas.Interfaces.Basic;
using trade_compas.Models;
using trade_compas.Utilities.DTOs.Order;

namespace trade_compas.Interfaces.Repositories;

public interface IOrdersRepository : IBaseRepository<Order>, ICreatable<CreateOrderDto>, IUpdatable<UpdateOrderDto>, IDeletable, IGetableBy<Order>
{
}