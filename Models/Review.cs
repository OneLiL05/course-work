using trade_compas.Interfaces.Basic;
using trade_compas.Utilities.DTOs.Comment;

namespace trade_compas.Models;

public class Review : CreateCommentDto, IIdentifiable, ITimestampable
{
    public Review() {}

    public Review(CreateCommentDto dto)
    {
        Id = ++lastId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Content = dto.Content;
        AuthorId = dto.AuthorId;
        ProductId = dto.ProductId;
        Stars = dto.Stars;
        Author = dto.Author;
    }

    private static int lastId = 0;
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsEdited { get; set; }
}