using System;
using MediatR;
using MediatR.CommandQuery.Mvc;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Controllers
{
    public class PriorityController
        : EntityCommandControllerBase<Guid, PriorityReadModel, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>
    {
        public PriorityController(IMediator mediator) : base(mediator)
        {

        }

    }
}
