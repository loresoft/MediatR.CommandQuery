namespace Tracker.WebService.Domain.Models
{
    public class RoleReadModel
        : EntityReadModel
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

    }
}
