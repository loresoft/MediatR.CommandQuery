using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class TaskQueryEndpoint : EntityQueryEndpointBase<Guid, TaskReadModel, TaskReadModel>
{
    public TaskQueryEndpoint(IMediator mediator) : base(mediator, "Task")
    {

    }

}

