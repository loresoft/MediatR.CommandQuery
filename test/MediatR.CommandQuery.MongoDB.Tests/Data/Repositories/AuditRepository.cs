using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

[RegisterSingleton]
public class AuditRepository : MongoEntityRepository<Audit>
{
    public AuditRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }

    protected override void EnsureIndexes(IMongoCollection<Audit> mongoCollection)
    {
        base.EnsureIndexes(mongoCollection);

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Audit>(
                Builders<Audit>.IndexKeys.Ascending(s => s.Date)
            )
        );

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Audit>(
                Builders<Audit>.IndexKeys.Ascending(s => s.UserId)
            )
        );

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Audit>(
                Builders<Audit>.IndexKeys.Ascending(s => s.TaskId)
            )
        );

        mongoCollection.Indexes.CreateOne(
            new CreateIndexModel<Audit>(
                Builders<Audit>.IndexKeys.Ascending(s => s.Username)
            )
        );
    }

}
