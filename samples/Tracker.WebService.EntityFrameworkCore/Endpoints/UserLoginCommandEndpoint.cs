using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class UserLoginCommandEndpoint : EntityCommandEndpointBase<Guid, UserLoginReadModel, UserLoginCreateModel, UserLoginUpdateModel>
{
    public UserLoginCommandEndpoint(IMediator mediator) : base(mediator, "UserLogin")
    {

    }

}

