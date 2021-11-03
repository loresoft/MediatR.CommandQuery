using MongoDB.Abstracts;
using MongoDB.Driver;

using Tracker.WebService.Data.Entities;

namespace Tracker.WebService.Data.Repositories
{
    public class AuditRepository : MongoEntityRepository<Audit>
    {
        public AuditRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
