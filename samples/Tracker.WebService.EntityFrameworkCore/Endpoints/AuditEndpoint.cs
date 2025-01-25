using System;
using MediatR;
using MediatR.CommandQuery.Endpoints;

using Microsoft.Extensions.Logging;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class AuditEndpoint : EntityCommandEndpointBase<Guid, AuditReadModel, AuditReadModel, AuditCreateModel, AuditUpdateModel>
{
    public AuditEndpoint(ILoggerFactory loggerFactory)
        : base(loggerFactory, "Audit")
    {

    }

}

