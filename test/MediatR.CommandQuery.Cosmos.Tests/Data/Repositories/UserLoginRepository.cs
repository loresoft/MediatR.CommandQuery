using Cosmos.Abstracts;

using MediatR.CommandQuery.Cosmos.Tests.Data.Entities;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Cosmos.Tests.Data.Repositories;

public class UserLoginRepository : CosmosRepository<UserLogin>
{
    public UserLoginRepository(ILoggerFactory logFactory, IOptions<CosmosRepositoryOptions> repositoryOptions, ICosmosFactory databaseFactory)
        : base(logFactory, repositoryOptions, databaseFactory)
    {
    }
}
