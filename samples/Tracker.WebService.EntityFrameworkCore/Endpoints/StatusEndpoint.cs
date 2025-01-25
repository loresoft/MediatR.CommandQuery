using System;

using MediatR.CommandQuery.Endpoints;

using Microsoft.Extensions.Logging;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class StatusEndpoint : EntityCommandEndpointBase<Guid, StatusReadModel, StatusReadModel, StatusCreateModel, StatusUpdateModel>
{
    public StatusEndpoint(ILoggerFactory loggerFactory)
        : base(loggerFactory, "Status")
    {

    }

}

