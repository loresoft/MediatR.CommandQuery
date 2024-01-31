using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class TaskEndpoint : EntityCommandEndpointBase<Guid, TaskReadModel, TaskReadModel, TaskCreateModel, TaskUpdateModel>
{
    public TaskEndpoint(IMediator mediator) : base(mediator, "Task")
    {

    }

}

