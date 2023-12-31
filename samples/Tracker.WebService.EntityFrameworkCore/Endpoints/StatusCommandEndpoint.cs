using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class StatusCommandEndpoint : EntityCommandEndpointBase<Guid, StatusReadModel, StatusCreateModel, StatusUpdateModel>
{
    public StatusCommandEndpoint(IMediator mediator) : base(mediator, "Status")
    {

    }

}

