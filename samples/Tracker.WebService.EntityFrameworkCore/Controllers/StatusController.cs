using System;
using MediatR;
using MediatR.CommandQuery.Mvc;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Controllers
{
    public class StatusController
        : EntityCommandControllerBase<Guid, StatusReadModel, StatusReadModel, StatusCreateModel, StatusUpdateModel>
    {
        public StatusController(IMediator mediator) : base(mediator)
        {

        }

    }
}
