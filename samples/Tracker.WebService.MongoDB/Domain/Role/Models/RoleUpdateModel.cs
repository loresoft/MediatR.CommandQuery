namespace Tracker.WebService.Domain.Models;

public class RoleUpdateModel
    : EntityUpdateModel
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

}
