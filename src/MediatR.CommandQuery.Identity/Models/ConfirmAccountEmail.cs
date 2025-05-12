// Ignore Spelling: Deserialize

namespace MediatR.CommandQuery.Identity.Models;

public class ConfirmAccountEmail : EmailModel
{
    public int ExpireHours { get; set; } = 24;
}
