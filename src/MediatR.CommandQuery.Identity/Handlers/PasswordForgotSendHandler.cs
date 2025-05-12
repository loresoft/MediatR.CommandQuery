using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Identity.Services;
using MediatR.CommandQuery.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Identity.Handlers;

public class PasswordForgotSendHandler<TUser> : RequestHandlerBase<PasswordForgotSendCommand, CompleteModel>
     where TUser : class
{
    private readonly UserManager<TUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IOptions<EnvironmentOptions> _environmentOptions;

    public PasswordForgotSendHandler(
        ILoggerFactory loggerFactory,
        UserManager<TUser> userManager,
        IEmailService emailService,
        IOptions<EnvironmentOptions> environmentOptions) : base(loggerFactory)
    {
        _userManager = userManager;
        _emailService = emailService;
        _environmentOptions = environmentOptions;
    }

    protected override async Task<CompleteModel> Process(PasswordForgotSendCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(true);
        if (user is null)
            return CompleteModel.Fail("Error: User does not exist");

        var recipientAddress = await _userManager.GetEmailAsync(user).ConfigureAwait(true);
        if (recipientAddress is null)
            return CompleteModel.Fail("Error: User doest not have a valid email address");

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(true);
        var userId = await _userManager.GetUserIdAsync(user).ConfigureAwait(true);

        var identityToken = new IdentityToken(userId, resetToken);
        var token = identityToken.Serialize();

        var resetUrl = RouteLinks.Account.ResetPassword(token);
        var linkUrl = _environmentOptions.Value.BaseAddress.Combine(resetUrl);

        var emailModel = new ResetPasswordEmail
        {
            Link = linkUrl,
            RecipientAddress = recipientAddress
        };

        return await _emailService.SendResetPasswordEmail(emailModel).ConfigureAwait(false);
    }
}
