using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class UserQueryEndpoint : EntityQueryEndpointBase<Guid, UserReadModel, UserReadModel>
{
    public UserQueryEndpoint(IMediator mediator) : base(mediator, "User")
    {

    }

}

