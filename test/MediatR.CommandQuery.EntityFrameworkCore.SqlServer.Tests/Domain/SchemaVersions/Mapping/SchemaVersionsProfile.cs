namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Mapping
{
    public partial class SchemaVersionsProfile
        : AutoMapper.Profile
    {
        public SchemaVersionsProfile()
        {
            CreateMap<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.SchemaVersions, MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models.SchemaVersionsReadModel>();

            CreateMap<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models.SchemaVersionsCreateModel, MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.SchemaVersions>();

            CreateMap<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.SchemaVersions, MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models.SchemaVersionsUpdateModel>();

            CreateMap<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models.SchemaVersionsUpdateModel, MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.SchemaVersions>();

            CreateMap<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models.SchemaVersionsReadModel, MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models.SchemaVersionsUpdateModel>();

        }

    }
}
