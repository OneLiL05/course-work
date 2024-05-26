using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Supabase.Gotrue;
using trade_compas.DTOs.Product;
using trade_compas.Interfaces;
using trade_compas.Interfaces.Repositories;
using trade_compas.Enums;
using trade_compas.Utilities.DTOs.Order;

namespace trade_compas.Controllers;

[Route("products")]
public class ProductsController(Supabase.Client supabaseClient, IProductsRepository productsRepository, ICategoriesRepository categoriesRepository, IOrdersRepository ordersRepository) : Controller
{
    private readonly User? _user = supabaseClient.Auth.CurrentUser;
    private readonly SelectList _categoriesList = new(categoriesRepository.GetAll(), "Slug", "Name");

    [HttpGet]
    public IActionResult Index(string searchQuery, string orderBy, SortingOrder order, string categorySlug)
    {
        var products = productsRepository.SortBy(
            productsRepository.GetAllBy(product => !product.InArchive),
            product => product.CreatedAt,
            SortingOrder.Desc);

        ViewBag.Categories = categoriesRepository.GetAll();
        ViewBag.SearchQuery = searchQuery;
        ViewBag.SortOrder = order;
        ViewBag.OrderBy = orderBy;
        ViewBag.CategorySlug = categorySlug;

        ViewData["User"] = _user;

        if (!string.IsNullOrEmpty(searchQuery))
        {
            products = productsRepository.SortBy(
                productsRepository.Search(p => p.Name, searchQuery),
                product => product.CreatedAt,
                SortingOrder.Desc
                );
        }

        if (!string.IsNullOrEmpty(categorySlug))
        {
            products = productsRepository.SortBy(
                productsRepository.Match(product => product.CategorySlug, categorySlug),
                product => product.CreatedAt,
                SortingOrder.Desc
            );
        }

        if (!string.IsNullOrEmpty(orderBy))
        {
            products = orderBy switch
            {
                "price" => productsRepository.SortBy(products, product => product.Price, order),
                "date" => productsRepository.SortBy(products, product => product.CreatedAt, order),
                _ => products
            };
        }

        return View(products);
    }

    [HttpGet("new")]
    public IActionResult New()
    {
        ViewData["User"] = _user;

        if (_user == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Category = _categoriesList;

        return View();
    }

    [HttpPost("new")]
    public IActionResult New(CreateProductDto dto)
    {
        ViewData["User"] = _user;

        if (ModelState.IsValid)
        {
            try
            {
                productsRepository.CreateOne(dto);

                return RedirectToAction("Index");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        var categories = categoriesRepository.GetAll();
        ViewBag.Category = new SelectList(categories, "Slug", "Name");

        return View(dto);
    }

    [HttpPost("{id:int}/archive")]
    public IActionResult Archive(int id)
    {
        productsRepository.Archive(id);

        return RedirectToAction("MyProducts", "Home");
    }

    [HttpPost("{id:int}/delete")]
    public IActionResult Delete(int id)
    {
        productsRepository.DeleteOne(id);

        return RedirectToAction("Profile", "Home");
    }

    [HttpPost("{id:int}/unarchive")]
    public IActionResult Unarchive(int id)
    {
        productsRepository.Unarchive(id);

        return RedirectToAction("MyArchive", "Home");
    }

    [HttpGet("{id:int}")]
    public IActionResult Details(int id)
    {
        if (_user == null)
        {
            return RedirectToAction("Index");
        }

        ViewData["User"] = _user;

        var product = productsRepository.GetOne(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpGet("{id:int}/edit")]
    public IActionResult Edit(int id)
    {
        ViewData["User"] = _user;

        var product = productsRepository.GetOne(id);

        ViewBag.Category = _categoriesList;

        if (product == null)
        {
            return NotFound();
        }

        if (_user == null)
        {
            return RedirectToAction("Index");
        }

        return View(product);
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult Edit(int id, CreateProductDto dto)
    {
        ViewData["User"] = _user;
        ViewBag.Category = _categoriesList;

        var product = productsRepository.GetOne(id);

        if (product == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            productsRepository.UpdateOne(id, dto);

            return RedirectToAction("Index");
        }

        return View(product);
    }

    [HttpGet("order/{id:int}")]
    public IActionResult Order(int id)
    {
        var product = productsRepository.GetOne(id);

        ViewData["User"] = _user;

        if (_user == null)
        {
            return RedirectToAction("Index");
        }

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.ProductId = product.Id;
        ViewBag.Price = product.Price;

        return View();
    }

    [HttpPost("order/{id:int}")]
    public IActionResult Order(int id, CreateOrderDto dto)
    {
        var product = productsRepository.GetOne(id);
        ViewData["User"] = _user;

        if (_user == null)
        {
            return RedirectToAction("Index");
        }

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.ProductId = product.Id;
        ViewBag.Price = product.Price;

        dto.Recipient.Id = _user.Id;
        dto.SellerId = product.SellerId;
        dto.ProductId = product.Id;

        if (ModelState.IsValid)
        {
            ordersRepository.CreateOne(dto);

            return RedirectToAction("Index");
        }

        return View(dto);
    }
}