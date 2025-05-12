using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Identity.Handlers;

public class PasswordResetHandler<TUser> : RequestHandlerBase<PasswordResetCommand, CompleteModel>
     where TUser : class
{
    private readonly UserManager<TUser> _userManager;

    public PasswordResetHandler(ILoggerFactory loggerFactory, UserManager<TUser> userManager) : base(loggerFactory)
    {
        _userManager = userManager;
    }

    protected override async Task<CompleteModel> Process(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var identityToken = IdentityToken.Deserialize(model.Token);

        var user = await _userManager.FindByIdAsync(identityToken.UserId).ConfigureAwait(false);
        if (user is null)
            return CompleteModel.Fail("Error: User does not exist");

        var result = await _userManager.ResetPasswordAsync(user, identityToken.Token, model.Password).ConfigureAwait(false);

        return new CompleteModel
        {
            Successful = result.Succeeded,
            Message = result.Errors?.Select(p => p.Description).ToDelimitedString(),
        };
    }
}
