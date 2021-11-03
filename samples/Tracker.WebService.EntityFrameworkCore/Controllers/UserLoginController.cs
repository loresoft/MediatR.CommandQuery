using System;
using MediatR;
using MediatR.CommandQuery.Mvc;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Controllers
{
    public class UserLoginController
        : EntityCommandControllerBase<Guid, UserLoginReadModel, UserLoginReadModel, UserLoginCreateModel, UserLoginUpdateModel>
    {
        public UserLoginController(IMediator mediator) : base(mediator)
        {

        }

    }
}
