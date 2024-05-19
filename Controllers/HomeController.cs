using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using trade_compas.Interfaces;
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

        return View();
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