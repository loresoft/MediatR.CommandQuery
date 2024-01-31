using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class UserEndpoint : EntityCommandEndpointBase<Guid, UserReadModel, UserReadModel, UserCreateModel, UserUpdateModel>
{
    public UserEndpoint(IMediator mediator) : base(mediator, "User")
    {

    }

}

