using System.ComponentModel.DataAnnotations;

namespace MediatR.CommandQuery.Identity.Models;

public partial class LoginLinkSendModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
}
