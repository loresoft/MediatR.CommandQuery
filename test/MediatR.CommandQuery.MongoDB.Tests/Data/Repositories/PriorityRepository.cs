using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

public class PriorityRepository : MongoEntityRepository<Priority>
{
    public PriorityRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
