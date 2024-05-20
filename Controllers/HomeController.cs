using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using trade_compas.Enums;
using trade_compas.Interfaces.Repositories;
using trade_compas.Models;
using User = Supabase.Gotrue.User;

namespace trade_compas.Controllers;

public class HomeController(Supabase.Client supabaseClient, IProductsRepository productsRepository)
    : Controller
{
    private readonly User? _user = supabaseClient.Auth.CurrentUser;

    public IActionResult Index()
    {
        ViewData["User"] = _user;

        return View();
    }

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
            productsRepository.GetArchive(_user.Id!),
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

        var products = productsRepository.GetUserProducts(_user.Id!);

        return View(products);
    }

    [HttpGet("/favourites")]
    public IActionResult Favourites()
    {
        var favourites = new List<Product>();

        return View(favourites);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}