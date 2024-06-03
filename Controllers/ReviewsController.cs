using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using trade_compas.Interfaces.Repositories;
using trade_compas.Models;
using trade_compas.Utilities.DTOs.Comment;

namespace trade_compas.Controllers;

public class ReviewsController(IReviewsRepository reviewsRepository, Supabase.Client supabaseClient) : Controller
{
    private readonly User? _user = supabaseClient.Auth.CurrentUser;

    [HttpPost]
    public IActionResult Create(int productId, string content, int stars)
    {
        if (!string.IsNullOrEmpty(content.Trim()))
        {
            var dto = new CreateCommentDto()
            {
                ProductId = productId,
                Content = content,
                Stars = stars,
                AuthorId = _user?.Id!,
                Author = _user!,
            };

            reviewsRepository.CreateOne(dto);
        }

        return RedirectToAction("Details", "Products", new { id = productId });
    }

    [HttpPost]
    public IActionResult Delete(int id, int productId)
    {
        reviewsRepository.DeleteOne(id);

        return RedirectToAction("Details", "Products", new { id = productId });
    }

    [HttpGet("/reviews/{id:int}/edit")]
    public IActionResult Edit(int id, int productId)
    {
        ViewData["User"] = _user;

        var review = reviewsRepository.GetOne(review => review.Id == id);

        if (review == null)
        {
            return RedirectToAction("Details", "Products", new { id = productId });
        }

        return View(review);
    }

    [HttpPost("/reviews/{id:int}/edit")]
    public IActionResult Edit(UpdateCommentDto dto, int id)
    {
        ViewData["User"] = _user;

        var review = reviewsRepository.GetOne(review => review.Id == id);

        var messages = string.Join("; ", ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage));

        Console.WriteLine(messages);

        if (ModelState.IsValid)
        {
            Console.WriteLine(dto.Content);

            reviewsRepository.UpdateOne(id, dto);

            return RedirectToAction("Details", "Products", new { id = review?.ProductId });
        }

        return View(review);
    }
}