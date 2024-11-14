using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;

using Microsoft.Extensions.Logging;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class RoleEndpoint : EntityCommandEndpointBase<Guid, RoleReadModel, RoleReadModel, RoleCreateModel, RoleUpdateModel>
{
    public RoleEndpoint(ILoggerFactory loggerFactory, IMediator mediator)
        : base(loggerFactory, mediator, "Role")
    {

    }

}

