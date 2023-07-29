using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Mapping;

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
