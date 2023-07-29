using MediatR;
using MediatR.CommandQuery.Mvc;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Controllers;

public class UserLoginController : EntityQueryControllerBase<string, UserLoginReadModel, UserLoginReadModel>
{
    public UserLoginController(IMediator mediator) : base(mediator)
    {
    }
}
