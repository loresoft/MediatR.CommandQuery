using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Mapping
{
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
}
