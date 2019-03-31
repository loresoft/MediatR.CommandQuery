using System;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Mapping
{
    public partial class TaskProfile
        : AutoMapper.Profile
    {
        public TaskProfile()
        {
            CreateMap<Data.Entities.Task, Models.TaskReadModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Models.TaskCreateModel, Data.Entities.Task>();

            CreateMap<Data.Entities.Task, Models.TaskUpdateModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Models.TaskUpdateModel, Data.Entities.Task>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));
        }

    }
}
