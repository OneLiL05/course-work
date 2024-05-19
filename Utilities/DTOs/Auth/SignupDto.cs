using System.ComponentModel.DataAnnotations;

namespace trade_compas.DTOs.Auth;

public class SignupDto
{
    [Required(ErrorMessage = "Email is required"), EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password should be at least 8 characters long")]
    public string Password { get; set; }
}