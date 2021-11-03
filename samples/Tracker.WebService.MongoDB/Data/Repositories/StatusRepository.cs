using Tracker.WebService.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace Tracker.WebService.Data.Repositories
{
    public class StatusRepository : MongoEntityRepository<Status>
    {
        public StatusRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
