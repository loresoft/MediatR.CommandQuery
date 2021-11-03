using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping
{
    public class StatusProfile
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
