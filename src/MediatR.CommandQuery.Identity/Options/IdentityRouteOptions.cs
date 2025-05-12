using MediatR.CommandQuery.Extensions;

namespace MediatR.CommandQuery.Identity.Options;

public class IdentityRouteOptions
{
    private const char separator = '/';

    public string BaseAddress { get; set; } = "/";

    public string ReturnDefault { get; set; } = "/";


    #region Account Routes
    public string AccountPrefix { get; set; } = "/Account";

    public string LoginRoute { get; set; } = "/Login";

    public string LogoutRoute { get; set; } = "/Logout";

    public string LockoutRoute { get; set; } = "/Lockout";

    public string RegisterRoute { get; set; } = "/Register";

    public string TwoFactorRoute { get; set; } = "/TwoFactor";

    public string ExternalChallengeRoute { get; set; } = "/ExternalChallenge";

    public string ExternalLoginRoute { get; set; } = "/ExternalLogin";



    public string LoginLink(string? returnUrl = null)
        => BuildReturn(AccountPrefix, LoginRoute, returnUrl);

    public string LogoutLink(string? returnUrl = null)
        => BuildReturn(AccountPrefix, LogoutRoute, returnUrl);

    public string TwoFactorLink()
        => AccountPrefix.Combine(TwoFactorRoute);

    public string ExternalChallengeLink()
        => AccountPrefix.Combine(ExternalChallengeRoute);


    public string ExternalLoginLink()
        => AccountPrefix.Combine(ExternalLoginRoute);

    #endregion

    #region Member Routes
    public string MemberPrefix { get; set; } = "/Member";

    public string ExternalLinkChallengeRoute { get; set; } = "/ExternalLink";

    public string MemberExternalLink()
        => MemberPrefix.Combine(ExternalLinkChallengeRoute);

    #endregion


    private static string BuildReturn(string prefix, string route, string? returnUrl)
    {
        var prefixSpan = prefix.AsSpan().Trim(separator);
        var routeSpan = route.AsSpan().Trim(separator);
        var returnSpan = Uri.EscapeDataString(returnUrl ?? "/");

        return $"/{prefixSpan}/{routeSpan}?returnUrl={returnSpan}";
    }
}
