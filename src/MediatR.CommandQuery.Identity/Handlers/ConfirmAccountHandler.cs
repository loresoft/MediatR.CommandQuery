using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Identity.Handlers;

public class ConfirmAccountHandler<TUser> : RequestHandlerBase<ConfirmAccountCommand, CompleteModel>
     where TUser : class
{
    private readonly UserManager<TUser> _userManager;

    public ConfirmAccountHandler(ILoggerFactory loggerFactory, UserManager<TUser> userManager) : base(loggerFactory)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    protected override async Task<CompleteModel> Process(ConfirmAccountCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var token = request.Token;
        if (token.IsNullOrEmpty())
            return CompleteModel.Fail("Error: Invalid user token");

        var identityToken = IdentityToken.Deserialize(token);

        var user = await _userManager.FindByIdAsync(identityToken.UserId).ConfigureAwait(false);
        if (user is null)
            return CompleteModel.Fail("Error: User does not exist");

        var result = await _userManager.ConfirmEmailAsync(user, identityToken.Token).ConfigureAwait(false);

        return new CompleteModel
        {
            Successful = result.Succeeded,
            Message = result.Errors?.Select(p => p.Description).ToDelimitedString(),
        };
    }
}
