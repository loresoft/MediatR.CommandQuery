using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Identity.Providers;

public class LoginLinkTokenProvider<TUser> : DataProtectorTokenProvider<TUser>
    where TUser : class
{
    public LoginLinkTokenProvider(
        IDataProtectionProvider dataProtectionProvider,
        IOptions<LoginLinkTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<TUser>> logger)
        : base(dataProtectionProvider, options, logger)
    {
    }
}
