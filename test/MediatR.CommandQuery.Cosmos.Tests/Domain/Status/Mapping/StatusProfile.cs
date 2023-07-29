using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Mapping;

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
