using Tracker.WebService.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace Tracker.WebService.Data.Repositories
{
    public class UserRepository : MongoEntityRepository<User>
    {
        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
