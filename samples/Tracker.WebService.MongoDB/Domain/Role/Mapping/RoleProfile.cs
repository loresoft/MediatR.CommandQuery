using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public class RoleProfile
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
