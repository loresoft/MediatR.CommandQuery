using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories
{
    public class TenantRepository : MongoEntityRepository<Tenant>
    {
        public TenantRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
