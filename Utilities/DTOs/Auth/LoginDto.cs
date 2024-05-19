using System.ComponentModel.DataAnnotations;

namespace trade_compas.DTOs.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required"), EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    // [HasSpecialSymbol(ErrorMessage = "Password should have at least one special symbol")]
    public string Password { get; set; }
}