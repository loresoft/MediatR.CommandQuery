using Cosmos.Abstracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Cosmos.Tests.Data.Repositories
{
    public class TaskRepository : CosmosRepository<Entities.Task>
    {
        public TaskRepository(ILoggerFactory logFactory, IOptions<CosmosRepositoryOptions> repositoryOptions, ICosmosFactory databaseFactory)
            : base(logFactory, repositoryOptions, databaseFactory)
        {
        }
    }
}