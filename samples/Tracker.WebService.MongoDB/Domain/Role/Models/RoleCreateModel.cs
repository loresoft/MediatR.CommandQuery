namespace Tracker.WebService.Domain.Models
{
    public class RoleCreateModel
        : EntityCreateModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}
