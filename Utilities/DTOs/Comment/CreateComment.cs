namespace trade_compas.Utilities.DTOs.Comment;

public class CreateCommentDto : UpdateCommentDto
{
    public string AuthorId { get; set; }
    public int ProductId { get; set; }
}