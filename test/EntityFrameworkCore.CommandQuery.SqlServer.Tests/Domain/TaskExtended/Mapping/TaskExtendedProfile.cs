using System;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Mapping
{
    public partial class TaskExtendedProfile
        : AutoMapper.Profile
    {
        public TaskExtendedProfile()
        {
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.TaskExtendedReadModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.TaskExtendedCreateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.TaskExtendedUpdateModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.TaskExtendedUpdateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended>();
        }

    }
}
