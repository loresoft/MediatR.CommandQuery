using Microsoft.AspNetCore.Identity;

namespace MediatR.CommandQuery.Identity.Providers;

public static class LoginLinkExtensions
{
    public static IdentityBuilder AddLoginLinkTokenProvider(this IdentityBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var userType = builder.UserType;
        var provider = typeof(LoginLinkTokenProvider<>).MakeGenericType(userType);

        return builder.AddTokenProvider(LoginLinkTokenProviderOptions.ProviderName, provider);
    }


    public static Task<string> GenerateLoginLinkTokenAsync<TUser>(this UserManager<TUser> userManager, TUser user)
        where TUser : class
    {
        ArgumentNullException.ThrowIfNull(userManager);
        ArgumentNullException.ThrowIfNull(user);

        return userManager.GenerateUserTokenAsync(
            user: user,
            tokenProvider: LoginLinkTokenProviderOptions.ProviderName,
            purpose: LoginLinkTokenProviderOptions.TokenPurpose);
    }

    public static Task<bool> VerifyLoginLinkTokenAsync<TUser>(this UserManager<TUser> userManager, TUser user, string token)
        where TUser : class
    {
        ArgumentNullException.ThrowIfNull(userManager);
        ArgumentNullException.ThrowIfNull(user);

        // Make sure the token is valid and the stamp matches
        return userManager.VerifyUserTokenAsync(
            user: user,
            tokenProvider: LoginLinkTokenProviderOptions.ProviderName,
            purpose: LoginLinkTokenProviderOptions.TokenPurpose,
            token: token);
    }

}
