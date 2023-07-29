namespace Tracker.WebService.Domain.Mapping;

public class TaskProfile
    : AutoMapper.Profile
{
    public TaskProfile()
    {
        CreateMap<Data.Entities.Task, Models.TaskReadModel>();

        CreateMap<Models.TaskCreateModel, Data.Entities.Task>();

        CreateMap<Models.TaskReadModel, Models.TaskUpdateModel>();

        CreateMap<Data.Entities.Task, Models.TaskUpdateModel>();

        CreateMap<Models.TaskUpdateModel, Data.Entities.Task>();
    }
}
