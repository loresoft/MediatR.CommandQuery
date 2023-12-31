using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class AuditCommandEndpoint : EntityCommandEndpointBase<Guid, AuditReadModel, AuditCreateModel, AuditUpdateModel>
{
    public AuditCommandEndpoint(IMediator mediator) : base(mediator, "Audit")
    {

    }

}

