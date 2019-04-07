using System;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Tenant.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Tenant.Mapping
{
    public partial class TenantProfile
        : AutoMapper.Profile
    {
        public TenantProfile()
        {
            CreateMap<Data.Entities.Tenant, TenantReadModel>();
            CreateMap<TenantCreateModel, Data.Entities.Tenant>();
            CreateMap<Data.Entities.Tenant, TenantUpdateModel>();
            CreateMap<TenantUpdateModel, Data.Entities.Tenant>();
        }

    }
}
