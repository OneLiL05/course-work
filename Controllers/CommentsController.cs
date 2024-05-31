using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using trade_compas.Interfaces.Repositories;
using trade_compas.Utilities.DTOs.Comment;

namespace trade_compas.Controllers;

public class CommentsController(IReviewsRepository reviewsRepository, Supabase.Client supabaseClient) : Controller
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
}