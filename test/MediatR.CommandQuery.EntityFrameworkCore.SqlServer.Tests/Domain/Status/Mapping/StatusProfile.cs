using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Status.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Status.Mapping;

public partial class StatusProfile
    : AutoMapper.Profile
{
    public StatusProfile()
    {
        CreateMap<Data.Entities.Status, StatusReadModel>();
        CreateMap<StatusCreateModel, Data.Entities.Status>();
        CreateMap<Data.Entities.Status, StatusUpdateModel>();
        CreateMap<StatusUpdateModel, Data.Entities.Status>();
    }

}
