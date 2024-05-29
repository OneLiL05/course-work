using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using trade_compas.Interfaces.Repositories;
using trade_compas.Utilities.DTOs.Comment;

namespace trade_compas.Controllers;

public class CommentsController(ICommentsRepository commentsRepository, Supabase.Client supabaseClient) : Controller
{
    private readonly User? _user = supabaseClient.Auth.CurrentUser;

    public IActionResult Create(int productId, string content, int grade)
    {
        if (!string.IsNullOrEmpty(content.Trim()))
        {
            var dto = new CreateCommentDto()
            {
                ProductId = productId,
                Content = content,
                Grade = grade,
                AuthorId = _user?.Id!,
            };

            commentsRepository.CreateOne(dto);
        }

        var comments = commentsRepository.GetAllBy(comment => comment.ProductId == productId);

        return PartialView("CommentsPartial", comments);
    }
}