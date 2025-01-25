using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;

using Microsoft.Extensions.Logging;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class UserLoginEndpoint : EntityCommandEndpointBase<Guid, UserLoginReadModel, UserLoginReadModel, UserLoginCreateModel, UserLoginUpdateModel>
{
    public UserLoginEndpoint(ILoggerFactory loggerFactory)
        : base(loggerFactory, "UserLogin")
    {

    }

}

