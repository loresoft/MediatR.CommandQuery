using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class StatusQueryEndpoint : EntityQueryEndpointBase<Guid, StatusReadModel, StatusReadModel>
{
    public StatusQueryEndpoint(IMediator mediator) : base(mediator, "Status")
    {

    }

}

