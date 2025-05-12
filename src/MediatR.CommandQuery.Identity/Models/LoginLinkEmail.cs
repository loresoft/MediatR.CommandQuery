// Ignore Spelling: Deserialize

namespace MediatR.CommandQuery.Identity.Models;

public class LoginLinkEmail : EmailModel
{
    public int ExpireMinutes { get; set; } = 15;
}
