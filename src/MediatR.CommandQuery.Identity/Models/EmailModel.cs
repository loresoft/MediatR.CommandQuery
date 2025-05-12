// Ignore Spelling: Deserialize

namespace MediatR.CommandQuery.Identity.Models;

public class EmailModel
{
    public string? FromName { get; set; }
    public string? FromAddress { get; set; }

    public string? RecipientName { get; set; }
    public string RecipientAddress { get; set; } = null!;

    public string? ReplyToName { get; set; }
    public string? ReplyToAddress { get; set; }

    public string Link { get; set; } = null!;

    public string ProductName { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
}
