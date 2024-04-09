using Cosmos.Abstracts;

using MediatR.CommandQuery.Cosmos.Tests.Data.Entities;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Task = System.Threading.Tasks.Task;

namespace MediatR.CommandQuery.Cosmos.Tests;

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
        var priorityRepository = _serviceProvider.GetRequiredService<ICosmosRepository<Priority>>();
        var statusRepository = _serviceProvider.GetRequiredService<ICosmosRepository<Status>>();
        var tenantRepository = _serviceProvider.GetRequiredService<ICosmosRepository<Tenant>>();
        var userRepository = _serviceProvider.GetRequiredService<ICosmosRepository<User>>();

        await priorityRepository.SaveAsync(Constants.PriorityConstants.High, cancellationToken);
        await priorityRepository.SaveAsync(Constants.PriorityConstants.Normal, cancellationToken);
        await priorityRepository.SaveAsync(Constants.PriorityConstants.Low, cancellationToken);

        await statusRepository.SaveAsync(Constants.StatusConstants.NotStarted, cancellationToken);
        await statusRepository.SaveAsync(Constants.StatusConstants.InProgress, cancellationToken);
        await statusRepository.SaveAsync(Constants.StatusConstants.Completed, cancellationToken);
        await statusRepository.SaveAsync(Constants.StatusConstants.Blocked, cancellationToken);
        await statusRepository.SaveAsync(Constants.StatusConstants.Deferred, cancellationToken);
        await statusRepository.SaveAsync(Constants.StatusConstants.Done, cancellationToken);

        await tenantRepository.SaveAsync(Constants.TenantConstants.Test, cancellationToken);

        await userRepository.SaveAsync(Constants.UserConstants.WilliamAdama, cancellationToken);
        await userRepository.SaveAsync(Constants.UserConstants.LauraRoslin, cancellationToken);
        await userRepository.SaveAsync(Constants.UserConstants.KaraThrace, cancellationToken);
        await userRepository.SaveAsync(Constants.UserConstants.LeeAdama, cancellationToken);
        await userRepository.SaveAsync(Constants.UserConstants.GaiusBaltar, cancellationToken);
        await userRepository.SaveAsync(Constants.UserConstants.SaulTigh, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
