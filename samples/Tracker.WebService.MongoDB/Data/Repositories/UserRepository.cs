using MongoDB.Abstracts;
using MongoDB.Driver;

using Tracker.WebService.Data.Entities;

namespace Tracker.WebService.Data.Repositories;

public class UserRepository : MongoEntityRepository<User>
{
    public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
