using System;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Mapping
{
    public partial class TenantProfile
        : AutoMapper.Profile
    {
        public TenantProfile()
        {
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.TenantReadModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.TenantCreateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.TenantUpdateModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.TenantUpdateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant>();
        }

    }
}
