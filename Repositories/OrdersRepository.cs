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
    private readonly GetAllByAction<Order> _getAllByAction = new();

    public List<Order> GetAll()
    {
        return FileHelper.LoadData<Order>(_collectionPath);
    }

    public Order? GetOne(int id)
    {
        return GetAll().Find(order => order.Id == id);
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
        var products = GetAll();

        _deleteAction.DoAction(products, id);

        FileHelper.SaveData(_collectionPath ,products);
    }
}