using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class PriorityQueryEndpoint : EntityQueryEndpointBase<Guid, PriorityReadModel, PriorityReadModel>
{
    public PriorityQueryEndpoint(IMediator mediator) : base(mediator, "Priority")
    {

    }

}

