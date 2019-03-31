using System;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Mapping
{
    public partial class RoleProfile
        : AutoMapper.Profile
    {
        public RoleProfile()
        {
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.RoleReadModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.RoleCreateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.RoleUpdateModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.RoleUpdateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role>();
        }

    }
}
