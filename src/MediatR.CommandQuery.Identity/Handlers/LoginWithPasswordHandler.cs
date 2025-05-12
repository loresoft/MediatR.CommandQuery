using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Identity.Handlers;

public class LoginWithPasswordHandler<TUser> : RequestHandlerBase<LoginWithPasswordCommand, LoginCompleteModel>
     where TUser : class
{
    private readonly SignInManager<TUser> _signInManager;

    public LoginWithPasswordHandler(ILoggerFactory loggerFactory, SignInManager<TUser> signInManager) : base(loggerFactory)
    {
        _signInManager = signInManager;
    }

    protected override async Task<LoginCompleteModel> Process(LoginWithPasswordCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        // Clear the existing external cookie to ensure a clean login process
        await _signInManager.Context.SignOutAsync(IdentityConstants.ExternalScheme).ConfigureAwait(false);

        var result = await _signInManager
            .PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true)
            .ConfigureAwait(false);

        return new LoginCompleteModel
        {
            Successful = result.Succeeded,
            IsLockedOut = result.IsLockedOut,
            IsNotAllowed = result.IsNotAllowed,
            RequiresTwoFactor = result.RequiresTwoFactor,
            Message = GetMessage(result)
        };
    }

    private static string GetMessage(SignInResult result)
    {
        return result.IsLockedOut ? "Locked Out" :
               result.IsNotAllowed ? "Not Allowed" :
               result.RequiresTwoFactor ? "Requires Two Factor" :
               result.Succeeded ? "Succeeded" : "Failed";
    }
}
