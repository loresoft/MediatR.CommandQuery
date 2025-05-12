using System.ComponentModel.DataAnnotations;

namespace MediatR.CommandQuery.Identity.Models;

public partial class LoginWithPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}
