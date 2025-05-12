using System.ComponentModel.DataAnnotations;

namespace MediatR.CommandQuery.Identity.Models;

public partial class PasswordForgotModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
}
