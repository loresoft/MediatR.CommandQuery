using System;
using MediatR;
using MediatR.CommandQuery.Mvc;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Controllers
{
    public class TaskController
        : EntityCommandControllerBase<Guid, TaskReadModel, TaskReadModel, TaskCreateModel, TaskUpdateModel>
    {
        public TaskController(IMediator mediator) : base(mediator)
        {

        }

    }
}
