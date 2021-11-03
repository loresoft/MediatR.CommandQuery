namespace Tracker.WebService.Domain.Models
{
    public class StatusUpdateModel
        : EntityUpdateModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

    }
}
