using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

public class UserLoginRepository : MongoEntityRepository<UserLogin>
{
    public UserLoginRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
