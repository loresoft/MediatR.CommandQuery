namespace Tracker.WebService.Domain.Models
{
    public class RoleUpdateModel
        : EntityUpdateModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}
