using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Identity.Options;
using MediatR.CommandQuery.Identity.Providers;
using MediatR.CommandQuery.Identity.Services;
using MediatR.CommandQuery.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Identity.Handlers;

public class LoginLinkSendHandler<TUser> : RequestHandlerBase<LoginLinkSendCommand, CompleteModel>
     where TUser : class
{
    private readonly UserManager<TUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IOptions<IdentityRouteOptions> _environmentOptions;

    public LoginLinkSendHandler(
        ILoggerFactory loggerFactory,
        UserManager<TUser> userManager,
        IEmailService emailService,
        IOptions<IdentityRouteOptions> environmentOptions) : base(loggerFactory)
    {
        _userManager = userManager;
        _emailService = emailService;
        _environmentOptions = environmentOptions;
    }

    protected override async Task<CompleteModel> Process(LoginLinkSendCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var model = request.Model;

        var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
        if (user is null)
            return CompleteModel.Fail("Error: User does not exist");

        if (_userManager.Options.SignIn.RequireConfirmedEmail && !await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false))
            return CompleteModel.Fail("Error: User is not confirmed");

        var recipientAddress = await _userManager.GetEmailAsync(user).ConfigureAwait(false);
        if (recipientAddress is null)
            return CompleteModel.Fail("Error: User doest not have a valid email address");

        var linkToken = await _userManager.GenerateLoginLinkTokenAsync(user).ConfigureAwait(false);
        var userId = await _userManager.GetUserIdAsync(user).ConfigureAwait(false);

        var identityToken = new IdentityToken(userId, linkToken);
        var token = identityToken.Serialize();

        var loginUrl = _environmentOptions.Value.LoginLink(token);
        var linkUrl = _environmentOptions.Value.BaseAddress.Combine(loginUrl);

        var emailModel = new LoginLinkEmail
        {
            Link = linkUrl,
            RecipientAddress = recipientAddress,
        };

        return await _emailService.SendLoginLinkEmail(emailModel).ConfigureAwait(false);
    }
}
