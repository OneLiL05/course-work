using System.ComponentModel.DataAnnotations;
using trade_compas.Interfaces.Basic;

namespace trade_compas.Models;

public class Recipient
{
    public string Id { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email should be valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Phone should be valid")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
}