using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class AuditQueryEndpoint : EntityQueryEndpointBase<Guid, AuditReadModel, AuditReadModel>
{
    public AuditQueryEndpoint(IMediator mediator) : base(mediator, "Audit")
    {

    }

}

