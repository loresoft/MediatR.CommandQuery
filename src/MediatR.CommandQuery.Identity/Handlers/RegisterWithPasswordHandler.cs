using AutoMapper;

using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Identity.Services;
using MediatR.CommandQuery.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Identity.Handlers;

public class RegisterWithPasswordHandler<TUser> : RequestHandlerBase<RegisterWithPasswordCommand, CompleteModel>
     where TUser : class, new()
{
    private readonly SignInManager<TUser> _signInManager;
    private readonly IUserStore<TUser> _userStore;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IOptions<EnvironmentOptions> _environmentOptions;

    public RegisterWithPasswordHandler(
        ILoggerFactory loggerFactory,
        SignInManager<TUser> signInManager,
        IUserStore<TUser> userStore,
        IMapper mapper,
        IEmailService emailService,
        IOptions<EnvironmentOptions> environmentOptions) : base(loggerFactory)
    {
        _signInManager = signInManager;
        _mapper = mapper;
        _emailService = emailService;
        _environmentOptions = environmentOptions;
        _userStore = userStore;
    }

    protected override async Task<CompleteModel> Process(RegisterWithPasswordCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        var user = await CreateUser(model, cancellationToken).ConfigureAwait(false);

        var userManager = _signInManager.UserManager;

        // create user
        var result = await userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
        if (!result.Succeeded)
            return CompleteModel.Fail($"Error: {result.Errors?.Select(p => p.Description).ToDelimitedString(" ")}");

        Logger.LogInformation("User '{Email}' created a new account with password.", model.Email);

        // sign user in
        await _signInManager.SignInAsync(user, isPersistent: true).ConfigureAwait(false);

        await SendConfirmationEmail(user, userManager).ConfigureAwait(false);

        return CompleteModel.Success();
    }

    private async Task SendConfirmationEmail(TUser user, UserManager<TUser> userManager)
    {
        try
        {
            // send confirm account email
            var userId = await userManager.GetUserIdAsync(user).ConfigureAwait(false);
            var userEmail = await userManager.GetEmailAsync(user).ConfigureAwait(false);

            if (userEmail.IsNullOrEmpty() || userId.IsNullOrEmpty())
                return;

            var confirmToken = await userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
            var identityToken = new IdentityToken(userId, confirmToken);
            var token = identityToken.Serialize();

            var confirmUrl = RouteLinks.Account.ConfirmAccount(token);
            var linkUrl = _environmentOptions.Value.BaseAddress.Combine(confirmUrl);

            var emailModel = new ConfirmAccountEmail
            {
                Link = linkUrl,
                RecipientAddress = userEmail
            };

            await _emailService.SendConfirmAccountEmail(emailModel).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // only log error
            Logger.LogError(ex, "Error sending email confirmation: {ErrorMessage}", ex.Message);
        }
    }

    private async Task<TUser> CreateUser(RegisterWithPasswordModel model, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<TUser>(model);

        // set identity properties, use stores to prevent multiple saves
        await _userStore.SetUserNameAsync(user, model.Email, cancellationToken).ConfigureAwait(false);

        if (_userStore is IUserEmailStore<TUser> userEmailStore)
            await userEmailStore.SetEmailAsync(user, model.Email, cancellationToken).ConfigureAwait(false);

        return user;
    }
}
