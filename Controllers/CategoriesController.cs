using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using trade_compas.Interfaces;
using trade_compas.Interfaces.Repositories;

namespace trade_compas.Controllers;

public class CategoriesController(IProductsRepository productsRepository, ICategoriesRepository categoriesRepository) : Controller
{
    [HttpGet("/categories")]
    public IActionResult Index()
    {
        var categories = categoriesRepository.GetAll();

        return View(categories);
    }

    [HttpGet("/categories/{categorySlug}")]
    public IActionResult Details(string slug)
    {
        var category = categoriesRepository.GetOne(category => category.Slug == slug);

        if (category == null)
        {
            return NotFound();
        }

        var products = productsRepository.GetAll();

        dynamic model = new ExpandoObject();

        model.Category = category;
        model.Products = products;

        return View(model);
    }
}