// Ignore Spelling: Deserialize

namespace MediatR.CommandQuery.Identity.Models;

public class ResetPasswordEmail : EmailModel
{
    public int ExpireHours { get; set; } = 24;
}
