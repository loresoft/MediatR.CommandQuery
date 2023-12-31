using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class UserLoginQueryEndpoint : EntityQueryEndpointBase<Guid, UserLoginReadModel, UserLoginReadModel>
{
    public UserLoginQueryEndpoint(IMediator mediator) : base(mediator, "UserLogin")
    {

    }

}

