using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Role.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Role.Mapping;

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
