using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class AuditEndpoint : EntityCommandEndpointBase<Guid, AuditReadModel, AuditReadModel, AuditCreateModel, AuditUpdateModel>
{
    public AuditEndpoint(IMediator mediator) : base(mediator, "Audit")
    {

    }

}

