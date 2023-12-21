using System;
using System.Threading;

using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

using Task = System.Threading.Tasks.Task;

namespace MediatR.CommandQuery.MongoDB.Tests;

public class DatabaseInitializer : IHostedService
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(ILogger<DatabaseInitializer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var priorityRepository = _serviceProvider.GetRequiredService<IMongoEntityRepository<Priority>>();
        await priorityRepository.UpsertAsync(Constants.PriorityConstants.High, cancellationToken);
        await priorityRepository.UpsertAsync(Constants.PriorityConstants.Normal, cancellationToken);
        await priorityRepository.UpsertAsync(Constants.PriorityConstants.Low, cancellationToken);

        var statusRepository = _serviceProvider.GetRequiredService<IMongoEntityRepository<Status>>();
        await statusRepository.UpsertAsync(Constants.StatusConstants.NotStarted, cancellationToken);
        await statusRepository.UpsertAsync(Constants.StatusConstants.InProgress, cancellationToken);
        await statusRepository.UpsertAsync(Constants.StatusConstants.Completed, cancellationToken);
        await statusRepository.UpsertAsync(Constants.StatusConstants.Blocked, cancellationToken);
        await statusRepository.UpsertAsync(Constants.StatusConstants.Deferred, cancellationToken);
        await statusRepository.UpsertAsync(Constants.StatusConstants.Done, cancellationToken);

        var tenantRepository = _serviceProvider.GetRequiredService<IMongoEntityRepository<Tenant>>();
        await tenantRepository.UpsertAsync(Constants.TenantConstants.Test, cancellationToken);

        var userRepository = _serviceProvider.GetRequiredService<IMongoEntityRepository<User>>();
        await userRepository.UpsertAsync(Constants.UserConstants.WilliamAdama, cancellationToken);
        await userRepository.UpsertAsync(Constants.UserConstants.LauraRoslin, cancellationToken);
        await userRepository.UpsertAsync(Constants.UserConstants.KaraThrace, cancellationToken);
        await userRepository.UpsertAsync(Constants.UserConstants.LeeAdama, cancellationToken);
        await userRepository.UpsertAsync(Constants.UserConstants.GaiusBaltar, cancellationToken);
        await userRepository.UpsertAsync(Constants.UserConstants.SaulTigh, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
