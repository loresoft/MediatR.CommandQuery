using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Identity.Providers;
using MediatR.CommandQuery.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Identity.Handlers;

public class LoginWithLinkHandler<TUser> : RequestHandlerBase<LoginWithLinkCommand, CompleteModel>
     where TUser : class
{
    private readonly SignInManager<TUser> _signInManager;

    public LoginWithLinkHandler(ILoggerFactory loggerFactory, SignInManager<TUser> signInManager) : base(loggerFactory)
    {
        _signInManager = signInManager;
    }

    protected override async Task<CompleteModel> Process(LoginWithLinkCommand request, CancellationToken cancellationToken)
    {
        var token = request.Token;
        if (token.IsNullOrEmpty())
            return CompleteModel.Fail("Error: Invalid user token");

        var identityToken = IdentityToken.Deserialize(token);

        var userManager = _signInManager.UserManager;

        var user = await userManager.FindByIdAsync(identityToken.UserId).ConfigureAwait(false);
        if (user is null)
            return CompleteModel.Fail("Error: User does not exist");


        var isValid = await userManager.VerifyLoginLinkTokenAsync(user, identityToken.Token).ConfigureAwait(false);
        if (!isValid)
            return CompleteModel.Fail("Error: Invalid user token");

        await _signInManager
            .SignInAsync(user, isPersistent: true)
            .ConfigureAwait(false);

        return CompleteModel.Success();
    }
}
