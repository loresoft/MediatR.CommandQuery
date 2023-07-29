using MongoDB.Abstracts;
using MongoDB.Driver;

using Tracker.WebService.Data.Entities;

namespace Tracker.WebService.Data.Repositories;

public class RoleRepository : MongoEntityRepository<Role>
{
    public RoleRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
