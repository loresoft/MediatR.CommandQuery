using MediatR;
using MediatR.CommandQuery.Mvc;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Controllers;

public class UserController : EntityCommandControllerBase<string, UserReadModel, UserReadModel, UserCreateModel, UserUpdateModel>
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }
}
