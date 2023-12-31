using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class UserCommandEndpoint : EntityCommandEndpointBase<Guid, UserReadModel, UserCreateModel, UserUpdateModel>
{
    public UserCommandEndpoint(IMediator mediator) : base(mediator, "User")
    {

    }

}

