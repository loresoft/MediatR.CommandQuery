using MediatR;
using MediatR.CommandQuery.Mvc;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Controllers;

public class AuditController : EntityCommandControllerBase<string, AuditReadModel, AuditReadModel, AuditCreateModel, AuditUpdateModel>
{
    public AuditController(IMediator mediator) : base(mediator)
    {
    }
}
