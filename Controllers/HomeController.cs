using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using trade_compas.Enums;
using trade_compas.Interfaces.Repositories;
using trade_compas.Models;
using User = Supabase.Gotrue.User;

namespace trade_compas.Controllers;

public class HomeController(Supabase.Client supabaseClient, IProductsRepository productsRepository, IOrdersRepository ordersRepository)
    : Controller
{
    private readonly User? _user = supabaseClient.Auth.CurrentUser;

    [HttpGet("/profile")]
    public IActionResult Profile()
    {
        ViewData["User"] = _user;

        return View(_user);
    }

    [HttpGet("/profile/archive")]
    public IActionResult MyArchive()
    {
        if (_user == null)
        {
            return RedirectToAction("Index");
        }

        ViewData["User"] = _user;

        var archivedProducts = productsRepository.SortBy(
            productsRepository.GetAllBy(product => product.SellerId == _user.Id && product.InArchive),
            product => product.CreatedAt,
            SortingOrder.Desc);

        return View(archivedProducts);
    }

    public IActionResult MyProducts()
    {
        if (_user == null)
        {
            return RedirectToAction("Index");
        }

        ViewData["User"] = _user;

        var products = productsRepository.GetAllBy(product => product.SellerId == _user.Id && !product.InArchive);

        return View(products);
    }

    [HttpGet("/profile/orders")]
    public IActionResult MyOrders()
    {
        if (_user == null)
        {
            return RedirectToAction("Index");
        }

        ViewData["User"] = _user;

        var classNames = new Dictionary<OrderStatus, string>()
        {
            { OrderStatus.Canceled , "text-bg-danger" },
            { OrderStatus.New, "text-bg-primary" },
            { OrderStatus.Sent, "text-bg-success" },
            { OrderStatus.ToSent, "text-bg-info" }
        };

        ViewBag.ClassNames = classNames;

        var orders = ordersRepository.GetAllBy(order => order.Recipient.Id == _user.Id);

        return View(orders);
    }

    [HttpGet("/profile/customer-orders")]
    public IActionResult CustomerOrders()
    {
        if (_user == null)
        {
            return RedirectToAction("Index");
        }

        ViewData["User"] = _user;

        var orders = ordersRepository.GetAllBy(order => order.Product.SellerId == _user.Id);

        return View(orders);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}