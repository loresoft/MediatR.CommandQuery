using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class RoleQueryEndpoint : EntityQueryEndpointBase<Guid, RoleReadModel, RoleReadModel>
{
    public RoleQueryEndpoint(IMediator mediator) : base(mediator, "Role")
    {

    }

}

