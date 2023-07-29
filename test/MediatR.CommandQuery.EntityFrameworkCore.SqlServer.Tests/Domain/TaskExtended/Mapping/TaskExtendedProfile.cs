using System;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.TaskExtended.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.TaskExtended.Mapping;

public partial class TaskExtendedProfile
    : AutoMapper.Profile
{
    public TaskExtendedProfile()
    {
        CreateMap<Data.Entities.TaskExtended, TaskExtendedReadModel>();
        CreateMap<TaskExtendedCreateModel, Data.Entities.TaskExtended>();
        CreateMap<Data.Entities.TaskExtended, TaskExtendedUpdateModel>();
        CreateMap<TaskExtendedUpdateModel, Data.Entities.TaskExtended>();
    }

}
