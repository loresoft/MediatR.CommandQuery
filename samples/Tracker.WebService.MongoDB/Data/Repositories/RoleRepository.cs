using Tracker.WebService.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace Tracker.WebService.Data.Repositories
{
    public class RoleRepository : MongoEntityRepository<Role>
    {
        public RoleRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
