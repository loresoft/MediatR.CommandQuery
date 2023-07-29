using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

public class StatusRepository : MongoEntityRepository<Status>
{
    public StatusRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
