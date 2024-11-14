using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;

using Microsoft.Extensions.Logging;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class StatusEndpoint : EntityCommandEndpointBase<Guid, StatusReadModel, StatusReadModel, StatusCreateModel, StatusUpdateModel>
{
    public StatusEndpoint(ILoggerFactory loggerFactory, IMediator mediator)
        : base(loggerFactory, mediator, "Status")
    {

    }

}

