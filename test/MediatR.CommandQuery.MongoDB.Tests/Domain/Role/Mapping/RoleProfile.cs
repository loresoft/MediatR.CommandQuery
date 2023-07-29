using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Mapping;

public partial class RoleProfile
    : AutoMapper.Profile
{
    public RoleProfile()
    {
        CreateMap<Data.Entities.Role, RoleReadModel>();
        CreateMap<RoleCreateModel, Data.Entities.Role>();
        CreateMap<Data.Entities.Role, RoleUpdateModel>();
        CreateMap<RoleUpdateModel, Data.Entities.Role>();
    }

}
