using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class PriorityEndpoint : EntityCommandEndpointBase<Guid, PriorityReadModel, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>
{
    public PriorityEndpoint(IMediator mediator) : base(mediator, "Priority")
    {

    }

}

