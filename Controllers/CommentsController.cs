using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using trade_compas.Interfaces.Repositories;
using trade_compas.Utilities.DTOs.Comment;

namespace trade_compas.Controllers;

public class CommentsController(ICommentsRepository commentsRepository, Supabase.Client supabaseClient) : Controller
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

            commentsRepository.CreateOne(dto);
        }

        var comments = commentsRepository.GetAllBy(comment => comment.ProductId == productId);

        return PartialView("CommentsPartial", comments);
    }

    [HttpPost]
    public IActionResult Delete(int id, int productId)
    {
        commentsRepository.DeleteOne(id);

        return RedirectToAction("Details", "Products", new { id = productId });
    }
}