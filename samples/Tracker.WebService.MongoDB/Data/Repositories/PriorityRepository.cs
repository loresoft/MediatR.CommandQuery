using MongoDB.Abstracts;
using MongoDB.Driver;

using Tracker.WebService.Data.Entities;

namespace Tracker.WebService.Data.Repositories;

public class PriorityRepository : MongoEntityRepository<Priority>
{
    public PriorityRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
