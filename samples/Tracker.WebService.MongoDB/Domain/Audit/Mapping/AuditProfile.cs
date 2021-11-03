namespace Tracker.WebService.Domain.Mapping
{
    public class AuditProfile
        : AutoMapper.Profile
    {
        public AuditProfile()
        {
            CreateMap<Data.Entities.Audit, Models.AuditReadModel>();

            CreateMap<Models.AuditReadModel, Models.AuditUpdateModel>();

            CreateMap<Models.AuditCreateModel, Data.Entities.Audit>();

            CreateMap<Data.Entities.Audit, Models.AuditUpdateModel>();

            CreateMap<Models.AuditUpdateModel, Data.Entities.Audit>();
        }
    }
}
