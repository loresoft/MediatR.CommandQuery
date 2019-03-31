using System;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Mapping
{
    public partial class UserLoginProfile
        : AutoMapper.Profile
    {
        public UserLoginProfile()
        {
            CreateMap<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserLogin, EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models.UserLoginReadModel>();
        }

    }
}
