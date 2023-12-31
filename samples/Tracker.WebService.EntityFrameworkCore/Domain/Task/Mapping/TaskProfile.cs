using System;

using AutoMapper;

using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public partial class TaskProfile
    : AutoMapper.Profile
{
    public TaskProfile()
    {
        CreateMap<Tracker.WebService.Data.Entities.Task, Tracker.WebService.Domain.Models.TaskReadModel>();

        CreateMap<Tracker.WebService.Domain.Models.TaskCreateModel, Tracker.WebService.Data.Entities.Task>();

        CreateMap<Tracker.WebService.Data.Entities.Task, Tracker.WebService.Domain.Models.TaskUpdateModel>();

        CreateMap<Tracker.WebService.Domain.Models.TaskUpdateModel, Tracker.WebService.Data.Entities.Task>();

        CreateMap<Tracker.WebService.Domain.Models.TaskReadModel, Tracker.WebService.Domain.Models.TaskUpdateModel>();

    }

}
