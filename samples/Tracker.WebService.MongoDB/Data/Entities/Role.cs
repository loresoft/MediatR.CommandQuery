using MediatR.CommandQuery.Definitions;

using MongoDB.Abstracts;

namespace Tracker.WebService.Data.Entities
{
    public class Role : MongoEntity, IHaveIdentifier<string>, ITrackCreated, ITrackUpdated
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
