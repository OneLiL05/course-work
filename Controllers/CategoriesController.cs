using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using trade_compas.Interfaces;

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
    public IActionResult Details(string categorySlug)
    {
        var category = categoriesRepository.GetOne(categorySlug);

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