using MongoDB.Abstracts;
using MongoDB.Driver;

namespace Tracker.WebService.Data.Repositories
{
    public class TaskRepository : MongoEntityRepository<Entities.Task>
    {
        public TaskRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
