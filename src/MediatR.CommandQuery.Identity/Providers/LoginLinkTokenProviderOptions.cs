using Microsoft.AspNetCore.Identity;

namespace MediatR.CommandQuery.Identity.Providers;

public class LoginLinkTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public const string ProviderName = "LoginLinkTokenProvider";
    public const string TokenPurpose = "LoginLink";


    public LoginLinkTokenProviderOptions()
    {
        Name = ProviderName;
        TokenLifespan = TimeSpan.FromMinutes(15);
    }
}
