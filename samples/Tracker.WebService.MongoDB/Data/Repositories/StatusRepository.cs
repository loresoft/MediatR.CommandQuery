using MongoDB.Abstracts;
using MongoDB.Driver;

using Tracker.WebService.Data.Entities;

namespace Tracker.WebService.Data.Repositories;

public class StatusRepository : MongoEntityRepository<Status>
{
    public StatusRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
