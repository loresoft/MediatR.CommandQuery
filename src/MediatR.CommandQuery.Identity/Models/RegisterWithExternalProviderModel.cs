using System.ComponentModel.DataAnnotations;

namespace MediatR.CommandQuery.Identity.Models;

public partial class RegisterWithExternalProviderModel
{
    [Required]
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;
}
