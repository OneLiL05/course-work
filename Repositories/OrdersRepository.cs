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
    private readonly UpdateAction<Order> _updateAction = new();

    public List<Order> GetAll()
    {
        return _getAllAction.DoAction(_collectionPath);
    }

    public Order? GetOne(Predicate<Order> match)
    {
        return _getOneAction.DoAction(GetAll(), match);
    }

    public List<Order> GetAllBy(Predicate<Order> match)
    {
        return _getAllByAction.DoAction(GetAll(), match);
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
        _updateAction.DoAction(
            _collectionPath,
            order => order.Id == id,
            order => order.Status = dto);
    }

    public void DeleteOne(int id)
    {
        _deleteAction.DoAction(_collectionPath, id);
    }
}