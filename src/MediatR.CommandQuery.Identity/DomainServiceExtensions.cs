using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Handlers;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR.CommandQuery.Identity;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddIdentityCommands<TUser>(this IServiceCollection services)
        where TUser : class, new()
    {
        services.TryAddTransient<IRequestHandler<RegisterWithPasswordCommand, IdentityResult>, RegisterWithPasswordHandler<TUser>>();

        services.TryAddTransient<IRequestHandler<LoginWithPasswordCommand, SignInResult>, LoginWithPasswordHandler<TUser>>();

        return services;
    }
}
