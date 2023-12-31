using System;

using AutoMapper;

using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public partial class StatusProfile
    : AutoMapper.Profile
{
    public StatusProfile()
    {
        CreateMap<Tracker.WebService.Data.Entities.Status, Tracker.WebService.Domain.Models.StatusReadModel>();

        CreateMap<Tracker.WebService.Domain.Models.StatusCreateModel, Tracker.WebService.Data.Entities.Status>();

        CreateMap<Tracker.WebService.Data.Entities.Status, Tracker.WebService.Domain.Models.StatusUpdateModel>();

        CreateMap<Tracker.WebService.Domain.Models.StatusUpdateModel, Tracker.WebService.Data.Entities.Status>();

        CreateMap<Tracker.WebService.Domain.Models.StatusReadModel, Tracker.WebService.Domain.Models.StatusUpdateModel>();

    }

}
