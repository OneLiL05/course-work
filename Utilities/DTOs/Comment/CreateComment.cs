using Supabase.Gotrue;

namespace trade_compas.Utilities.DTOs.Comment;

public class CreateCommentDto : UpdateCommentDto
{
    public User Author { get; set; }
    public string AuthorId { get; set; }
    public int ProductId { get; set; }
}