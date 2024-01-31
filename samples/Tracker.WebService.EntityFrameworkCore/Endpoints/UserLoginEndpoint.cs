using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class UserLoginEndpoint : EntityCommandEndpointBase<Guid, UserLoginReadModel, UserLoginReadModel, UserLoginCreateModel, UserLoginUpdateModel>
{
    public UserLoginEndpoint(IMediator mediator) : base(mediator, "UserLogin")
    {

    }

}

