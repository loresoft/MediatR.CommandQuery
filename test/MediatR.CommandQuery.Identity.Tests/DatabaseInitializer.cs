using MediatR.CommandQuery.Identity.Tests.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Identity.Tests;

public class DatabaseInitializer : IHostedService
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IdentityContext identityContext;

    public DatabaseInitializer(ILogger<DatabaseInitializer> logger, IdentityContext identityContext)
    {
        _logger = logger;
        this.identityContext = identityContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Apply Database migrations");

        await identityContext.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
