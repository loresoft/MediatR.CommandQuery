namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Mapping;

public partial class PriorityProfile
    : AutoMapper.Profile
{
    public PriorityProfile()
    {
        CreateMap<Data.Entities.Priority, Models.PriorityReadModel>();

        CreateMap<Models.PriorityCreateModel, Data.Entities.Priority>();

        CreateMap<Data.Entities.Priority, Models.PriorityUpdateModel>();

        CreateMap<Models.PriorityUpdateModel, Data.Entities.Priority>();
    }

}
