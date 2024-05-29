using trade_compas.Interfaces.Basic;
using trade_compas.Utilities.DTOs.Comment;

namespace trade_compas.Models;

public class Comment : CreateCommentDto, IIdentifiable, ITimestampable
{
    public Comment() {}

    public Comment(CreateCommentDto dto)
    {
        Id = ++lastId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Content = dto.Content;
        AuthorId = dto.AuthorId;
        ProductId = dto.ProductId;
        Grade = dto.Grade;
    }

    private static int lastId = 0;
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}