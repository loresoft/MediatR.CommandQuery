using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

[RegisterSingleton]
public class TenantRepository : MongoEntityRepository<Tenant>
{
    public TenantRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }

    protected override void EnsureIndexes(IMongoCollection<Tenant> mongoCollection)
    {
        base.EnsureIndexes(mongoCollection);

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Tenant>(
                Builders<Tenant>.IndexKeys.Ascending(s => s.Name)
            )
        );

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Tenant>(
                Builders<Tenant>.IndexKeys.Ascending(s => s.IsActive)
            )
        );

    }

}
