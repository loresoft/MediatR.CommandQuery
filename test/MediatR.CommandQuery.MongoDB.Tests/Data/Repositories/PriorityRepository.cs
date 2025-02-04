using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

[RegisterSingleton]
public class PriorityRepository : MongoEntityRepository<Priority>
{
    public PriorityRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
    protected override void EnsureIndexes(IMongoCollection<Priority> mongoCollection)
    {
        base.EnsureIndexes(mongoCollection);

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Priority>(
                Builders<Priority>.IndexKeys.Ascending(s => s.Name)
            )
        );

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Priority>(
                Builders<Priority>.IndexKeys.Ascending(s => s.IsActive)
            )
        );
    }

}
