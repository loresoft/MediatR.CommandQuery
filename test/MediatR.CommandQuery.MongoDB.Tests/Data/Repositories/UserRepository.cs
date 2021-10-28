using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories
{
    public class UserRepository : MongoEntityRepository<User>
    {
        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
