using trade_compas.Enums;
using trade_compas.Interfaces.Helpers;
using trade_compas.Interfaces.Repositories;
using trade_compas.Models;
using trade_compas.Utilities.Actions;
using trade_compas.Utilities.DTOs.Order;
using trade_compas.Utils;

namespace trade_compas.Repositories;

public class OrdersRepository(IPathHelper pathHelper) : IOrdersRepository
{
    private readonly string _collectionPath = pathHelper.GetCollectionPath("orders");
    private readonly DeleteAction<Order> _deleteAction = new();
    private readonly GetAllAction<Order> _getAllAction = new();
    private readonly GetAllByAction<Order> _getAllByAction = new();
    private readonly GetOneAction<Order> _getOneAction = new();

    public List<Order> GetAll()
    {
        return _getAllAction.DoAction(_collectionPath);
    }

    public Order? GetOne(Func<Order, bool> predicate)
    {
        return _getOneAction.DoAction(GetAll(), predicate);
    }

    public List<Order> GetAllBy(Func<Order, bool> predicate)
    {
        return _getAllByAction.DoAction(GetAll(), predicate);
    }

    public void CreateOne(CreateOrderDto dto)
    {
        var orders = GetAll();

        var order = new Order(dto);

        orders.Add(order);

        FileHelper.SaveData(_collectionPath, orders);
    }

    public void UpdateOne(int id, OrderStatus dto)
    {
        var orders = GetAll();

        orders
            .Where(order => order.Id == id)
            .ToList()
            .ForEach(order => order.Status = dto);

        FileHelper.SaveData(_collectionPath, orders);
    }

    public void DeleteOne(int id)
    {
        _deleteAction.DoAction(_collectionPath, id);
    }
}