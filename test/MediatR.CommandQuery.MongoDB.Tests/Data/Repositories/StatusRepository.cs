using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

[RegisterSingleton]
public class StatusRepository : MongoEntityRepository<Status>
{
    public StatusRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }

    protected override void EnsureIndexes(IMongoCollection<Status> mongoCollection)
    {
        base.EnsureIndexes(mongoCollection);

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Status>(
                Builders<Status>.IndexKeys.Ascending(s => s.Name)
            ),
            new CreateOneIndexOptions { }
        );

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Status>(
                Builders<Status>.IndexKeys.Ascending(s => s.IsActive)
            )
        );

    }

}
