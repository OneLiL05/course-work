using System.ComponentModel.DataAnnotations;

namespace trade_compas.Utilities.DTOs.Comment;

public class CreateCommentDto
{
    public string AuthorId { get; set; }
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Message is required")]
    [StringLength(256, ErrorMessage = "M")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Grade is required")]
    [Range(1, 5)]
    public int Grade { get; set; }
}