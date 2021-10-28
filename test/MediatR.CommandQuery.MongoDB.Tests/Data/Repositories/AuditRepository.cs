using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories
{
    public class AuditRepository : MongoEntityRepository<Audit>
    {
        public AuditRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
