using System.ComponentModel.DataAnnotations;

namespace trade_compas.Utilities.DTOs.Comment;

public class UpdateCommentDto
{
    [Required(ErrorMessage = "Message is required")]
    [StringLength(256, ErrorMessage = "M")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Grade is required")]
    [Range(1, 5)]
    public int Stars { get; set; }
}