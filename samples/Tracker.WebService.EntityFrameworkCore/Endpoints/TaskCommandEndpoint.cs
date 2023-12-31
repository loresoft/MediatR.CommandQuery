using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class TaskCommandEndpoint : EntityCommandEndpointBase<Guid, TaskReadModel, TaskCreateModel, TaskUpdateModel>
{
    public TaskCommandEndpoint(IMediator mediator) : base(mediator, "Task")
    {

    }

}

