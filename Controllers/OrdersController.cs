using Microsoft.AspNetCore.Mvc;
using trade_compas.Enums;
using trade_compas.Interfaces.Repositories;
using trade_compas.Utilities.DTOs.Order;
using Supabase.Gotrue;

namespace trade_compas.Controllers;

public class OrdersController(Supabase.Client supabaseClient, IOrdersRepository ordersRepository) : Controller
{
    private readonly User? _user = supabaseClient.Auth.CurrentUser;

    [HttpPost]
    public IActionResult Update(int id, OrderStatus status)
    {
        var dto = new UpdateOrderDto(status);

        ordersRepository.UpdateOne(id, dto);

        return RedirectToAction("CustomerOrders", "Home");
    }
}