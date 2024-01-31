using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class StatusEndpoint : EntityCommandEndpointBase<Guid, StatusReadModel, StatusReadModel, StatusCreateModel, StatusUpdateModel>
{
    public StatusEndpoint(IMediator mediator) : base(mediator, "Status")
    {

    }

}

