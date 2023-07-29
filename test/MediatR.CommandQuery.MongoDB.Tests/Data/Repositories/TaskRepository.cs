using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories;

public class TaskRepository : MongoEntityRepository<Entities.Task>
{
    public TaskRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}
