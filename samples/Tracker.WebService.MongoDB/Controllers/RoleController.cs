using MediatR;
using MediatR.CommandQuery.Mvc;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Controllers;

public class RoleController : EntityCommandControllerBase<string, RoleReadModel, RoleReadModel, RoleCreateModel, RoleUpdateModel>
{
    public RoleController(IMediator mediator) : base(mediator)
    {
    }
}
