using MediatR;
using MediatR.CommandQuery.Models;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Tracker.WebService.Domain.Commands;
using Tracker.WebService.Domain.Handlers;

// ReSharper disable once CheckNamespace
namespace Tracker.WebService.Domain;

public class JobServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.TryAddTransient<IRequestHandler<BackgroundUpdateCommand, CompleteModel>, BackgroundUpdateHandler>();
    }

}
