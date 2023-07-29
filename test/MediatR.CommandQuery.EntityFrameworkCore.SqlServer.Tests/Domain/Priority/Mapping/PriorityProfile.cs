using System;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority.Mapping;

public partial class PriorityProfile
    : AutoMapper.Profile
{
    public PriorityProfile()
    {
        CreateMap<Data.Entities.Priority, Models.PriorityReadModel>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

        CreateMap<Models.PriorityCreateModel, Data.Entities.Priority>();

        CreateMap<Data.Entities.Priority, Models.PriorityUpdateModel>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

        CreateMap<Models.PriorityUpdateModel, Data.Entities.Priority>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));
    }

}
