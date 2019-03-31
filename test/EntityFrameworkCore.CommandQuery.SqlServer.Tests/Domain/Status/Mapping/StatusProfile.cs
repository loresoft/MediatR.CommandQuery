using System;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Mapping
{
    public partial class StatusProfile
        : AutoMapper.Profile
    {
        public StatusProfile()
        {
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Status, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.StatusReadModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.StatusCreateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Status>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Status, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.StatusUpdateModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.StatusUpdateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Status>();
        }

    }
}
