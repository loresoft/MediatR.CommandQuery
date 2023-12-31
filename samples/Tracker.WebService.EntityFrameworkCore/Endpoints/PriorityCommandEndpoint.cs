using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class PriorityCommandEndpoint : EntityCommandEndpointBase<Guid, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>
{
    public PriorityCommandEndpoint(IMediator mediator) : base(mediator, "Priority")
    {

    }

}

