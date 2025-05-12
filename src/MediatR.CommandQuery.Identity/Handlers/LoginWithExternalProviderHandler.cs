using System.Security.Claims;

using AutoMapper;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Identity.Handlers;

public class LoginWithExternalProviderHandler<TUser> : RequestHandlerBase<LoginWithExternalProviderCommand, LoginCompleteModel>
     where TUser : class
{
    private readonly SignInManager<TUser> _signInManager;
    private readonly IUserStore<TUser> _userStore;
    private readonly IMapper _mapper;
    private readonly IPrincipalReader _principalReader;

    public LoginWithExternalProviderHandler(
        ILoggerFactory loggerFactory,
        SignInManager<TUser> signInManager,
        IUserStore<TUser> userStore,
        IMapper mapper,
        IPrincipalReader principalReader) : base(loggerFactory)
    {
        _signInManager = signInManager;
        _userStore = userStore;
        _mapper = mapper;
        _principalReader = principalReader;
    }

    protected override async Task<LoginCompleteModel> Process(LoginWithExternalProviderCommand request, CancellationToken cancellationToken)
    {
        if (request.RemoteError.HasValue())
            return LoginCompleteModel.Fail(request.RemoteError);

        var externalLogin = await _signInManager.GetExternalLoginInfoAsync().ConfigureAwait(false);
        if (externalLogin == null)
            return LoginCompleteModel.Fail("Error: Could not load external login information.");

        // Sign in the user with this external login provider if the user already has a login.
        var externalSignInResult = await _signInManager
            .ExternalLoginSignInAsync(
                externalLogin.LoginProvider,
                externalLogin.ProviderKey,
                isPersistent: true,
                bypassTwoFactor: true
            )
            .ConfigureAwait(false);

        if (externalSignInResult.Succeeded || externalSignInResult.IsLockedOut)
            return new LoginCompleteModel
            {
                Successful = externalSignInResult.Succeeded,
                IsLockedOut = externalSignInResult.IsLockedOut,
                IsNotAllowed = externalSignInResult.IsNotAllowed,
                RequiresTwoFactor = externalSignInResult.RequiresTwoFactor,
                Message = GetMessage(externalSignInResult)
            };

        // external user doesn't exist, create in identity
        var user = await CreateUser(externalLogin.Principal, cancellationToken).ConfigureAwait(false);

        var userManager = _signInManager.UserManager;

        var identityResult = await userManager.CreateAsync(user).ConfigureAwait(false);
        if (!identityResult.Succeeded)
            return LoginCompleteModel.Fail($"Error: {identityResult.Errors?.Select(p => p.Description).ToDelimitedString(" ")}");

        var loginResult = await userManager.AddLoginAsync(user, externalLogin).ConfigureAwait(false);
        if (!loginResult.Succeeded)
            return LoginCompleteModel.Fail($"Error: {loginResult.Errors?.Select(p => p.Description).ToDelimitedString(" ")}");

        await _signInManager.SignInAsync(user, isPersistent: true, externalLogin.LoginProvider).ConfigureAwait(false);

        return LoginCompleteModel.Success();
    }

    private async Task<TUser> CreateUser(ClaimsPrincipal principal, CancellationToken cancellationToken)
    {
        // If the user does not have an account, create an account.
        string email = _principalReader.GetEmail(principal) ?? $"anonymous.{Random.Shared.Alphanumeric(8)}@example.com";
        string name = _principalReader.GetName(principal) ?? email;

        var model = new RegisterWithExternalProviderModel
        {
            DisplayName = name,
            Email = email,
        };

        var user = _mapper.Map<TUser>(model);

        // set identity properties, use stores to prevent multiple saves
        await _userStore.SetUserNameAsync(user, model.Email, cancellationToken).ConfigureAwait(false);

        if (_userStore is IUserEmailStore<TUser> userEmailStore)
            await userEmailStore.SetEmailAsync(user, model.Email, cancellationToken).ConfigureAwait(false);

        return user;
    }

    private static string GetMessage(SignInResult result)
    {
        return result.IsLockedOut ? "Locked Out" :
               result.IsNotAllowed ? "Not Allowed" :
               result.RequiresTwoFactor ? "Requires Two Factor" :
               result.Succeeded ? "Succeeded" : "Failed";
    }

}
