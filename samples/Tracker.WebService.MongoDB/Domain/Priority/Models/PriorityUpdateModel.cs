namespace Tracker.WebService.Domain.Models;

public class PriorityUpdateModel
    : EntityUpdateModel
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

}
