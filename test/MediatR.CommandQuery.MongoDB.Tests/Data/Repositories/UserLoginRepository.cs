using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

[RegisterSingleton]
public class UserLoginRepository : MongoEntityRepository<UserLogin>
{
    public UserLoginRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }

    protected override void EnsureIndexes(IMongoCollection<UserLogin> mongoCollection)
    {
        base.EnsureIndexes(mongoCollection);

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<UserLogin>(
                Builders<UserLogin>.IndexKeys.Ascending(s => s.EmailAddress)
            )
        );

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<UserLogin>(
                Builders<UserLogin>.IndexKeys.Ascending(s => s.UserId)
            )
        );
    }

}
