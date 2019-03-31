using System;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Mapping
{
    public partial class UserProfile
        : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.UserReadModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.UserCreateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.UserUpdateModel>();
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.UserUpdateModel, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User>();
        }

    }
}
