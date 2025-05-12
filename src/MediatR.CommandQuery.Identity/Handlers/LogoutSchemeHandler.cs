using MediatR.CommandQuery.Handlers;
using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Identity.Handlers;

public class LogoutSchemeHandler<TUser> : RequestHandlerBase<LogoutSchemeCommand, CompleteModel>
     where TUser : class
{
    private readonly SignInManager<TUser> _signInManager;

    public LogoutSchemeHandler(ILoggerFactory loggerFactory, SignInManager<TUser> signInManager) : base(loggerFactory)
    {
        _signInManager = signInManager;
    }

    protected override async Task<CompleteModel> Process(LogoutSchemeCommand request, CancellationToken cancellationToken)
    {
        foreach (var scheme in request.Schemes)
            await _signInManager.Context.SignOutAsync(scheme).ConfigureAwait(false);

        return CompleteModel.Success();
    }
}
