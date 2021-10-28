using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using MongoDB.Abstracts;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Repositories
{
    public class RoleRepository : MongoEntityRepository<Role>
    {
        public RoleRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
