namespace Tracker.WebService.Domain.Models
{
    public class StatusCreateModel
        : EntityCreateModel
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

    }
}
