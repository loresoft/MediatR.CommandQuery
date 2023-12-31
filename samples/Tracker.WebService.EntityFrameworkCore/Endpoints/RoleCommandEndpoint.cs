using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class RoleCommandEndpoint : EntityCommandEndpointBase<Guid, RoleReadModel, RoleCreateModel, RoleUpdateModel>
{
    public RoleCommandEndpoint(IMediator mediator) : base(mediator, "Role")
    {

    }

}

