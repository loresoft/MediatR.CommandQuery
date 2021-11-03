using Tracker.WebService.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace Tracker.WebService.Data.Repositories
{
    public class PriorityRepository : MongoEntityRepository<Priority>
    {
        public PriorityRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
