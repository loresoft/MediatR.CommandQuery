using System;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.User.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.User.Mapping;

public partial class UserProfile
    : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<Data.Entities.User, UserReadModel>();
        CreateMap<UserCreateModel, Data.Entities.User>();
        CreateMap<Data.Entities.User, UserUpdateModel>();
        CreateMap<UserUpdateModel, Data.Entities.User>();
    }

}
