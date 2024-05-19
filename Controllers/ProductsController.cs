using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Supabase.Gotrue;
using trade_compas.DTOs.Product;
using trade_compas.Interfaces;
using trade_compas.Models;
using trade_compas.Enums;

namespace trade_compas.Controllers;

[Route("products")]
public class ProductsController(Supabase.Client supabaseClient, IProductsRepository productsRepository, ICategoriesRepository categoriesRepository) : Controller
{
    private readonly User? _user = supabaseClient.Auth.CurrentUser;
    private readonly SelectList _categoriesList = new(categoriesRepository.GetAll(), "Slug", "Name");

    [HttpGet]
    public IActionResult Index(string searchQuery, string orderBy, SortingOrder order, string categorySlug)
    {
        var products = productsRepository.GetAll();

        ViewBag.Categories = categoriesRepository.GetAll();
        ViewBag.SearchQuery = searchQuery;
        ViewBag.SortOrder = order;
        ViewBag.OrderBy = orderBy;
        ViewBag.CategorySlug = categorySlug;

        ViewData["User"] = _user;

        if (!string.IsNullOrEmpty(searchQuery))
        {
            products = productsRepository.Search(p => p.Name, searchQuery);
        }

        if (!string.IsNullOrEmpty(categorySlug))
        {
            products = productsRepository.Match(product => product.CategorySlug, categorySlug);
        }

        if (!string.IsNullOrEmpty(orderBy))
        {
            Console.WriteLine(order);

            products = orderBy switch
            {
                "price" => productsRepository.SortBy(product => product.Price, order),
                "date" => productsRepository.SortBy(product => product.CreatedAt, order),
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

    [HttpGet("{id:int}")]
    public IActionResult Details(int id)
    {
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
        var product = productsRepository.GetOne(id);
        ViewBag.Category = _categoriesList;

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
}