using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

[RegisterSingleton]
public class UserRepository : MongoEntityRepository<User>
{
    public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }


    protected override void EnsureIndexes(IMongoCollection<User> mongoCollection)
    {
        base.EnsureIndexes(mongoCollection);

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(s => s.EmailAddress),
                new CreateIndexOptions { Unique = true }
            )
        );
    }
}
