using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

[RegisterSingleton]
public class RoleRepository : MongoEntityRepository<Role>
{
    public RoleRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }

    protected override void EnsureIndexes(IMongoCollection<Role> mongoCollection)
    {
        base.EnsureIndexes(mongoCollection);

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Role>(
                Builders<Role>.IndexKeys.Ascending(s => s.Name)
            )
        );
    }
}
