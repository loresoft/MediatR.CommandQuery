namespace Tracker.WebService.Domain.Models;

public class RoleCreateModel
    : EntityCreateModel
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

}
