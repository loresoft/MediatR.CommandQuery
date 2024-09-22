using System.Threading;
using System.Threading.Tasks;

using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Models;

using Microsoft.Extensions.Logging;

using Tracker.WebService.Domain.Commands;

namespace Tracker.WebService.Domain.Handlers;

public class BackgroundUpdateHandler : RequestHandlerBase<BackgroundUpdateCommand, CompleteModel>
{
    public BackgroundUpdateHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    protected override async Task<CompleteModel> Process(BackgroundUpdateCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Process Background Job: {Request}", request);

        await Task.Delay(500, cancellationToken);

        return new CompleteModel { Successful = true };
    }
}
