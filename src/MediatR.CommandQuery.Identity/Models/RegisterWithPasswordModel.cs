using System.ComponentModel.DataAnnotations;

namespace MediatR.CommandQuery.Identity.Models;

public partial class RegisterWithPasswordModel
{
    [Required]
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Password Confirmation")]
    [Compare("Password", ErrorMessage = "The Password and Password Confirmation do not match.")]
    public string PasswordConfirmation { get; set; } = null!;
}
