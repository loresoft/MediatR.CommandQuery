using Tracker.WebService.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace Tracker.WebService.Data.Repositories
{
    public class UserLoginRepository : MongoEntityRepository<UserLogin>
    {
        public UserLoginRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
