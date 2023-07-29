using MongoDB.Abstracts;
using MongoDB.Driver;

using Tracker.WebService.Data.Entities;

namespace Tracker.WebService.Data.Repositories;

public class UserLoginRepository : MongoEntityRepository<UserLogin>
{
    public UserLoginRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
