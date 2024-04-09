using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.UserLogin.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.UserLogin.Mapping;

public partial class UserLoginProfile
    : AutoMapper.Profile
{
    public UserLoginProfile()
    {
        CreateMap<Data.Entities.UserLogin, UserLoginReadModel>();
    }

}
