using System;

using AutoMapper;

using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public partial class PriorityProfile
    : AutoMapper.Profile
{
    public PriorityProfile()
    {
        CreateMap<Tracker.WebService.Data.Entities.Priority, Tracker.WebService.Domain.Models.PriorityReadModel>();

        CreateMap<Tracker.WebService.Domain.Models.PriorityCreateModel, Tracker.WebService.Data.Entities.Priority>();

        CreateMap<Tracker.WebService.Data.Entities.Priority, Tracker.WebService.Domain.Models.PriorityUpdateModel>();

        CreateMap<Tracker.WebService.Domain.Models.PriorityUpdateModel, Tracker.WebService.Data.Entities.Priority>();

        CreateMap<Tracker.WebService.Domain.Models.PriorityReadModel, Tracker.WebService.Domain.Models.PriorityUpdateModel>();

    }

}
