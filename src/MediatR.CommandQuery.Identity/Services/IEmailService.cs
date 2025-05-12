using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.Identity.Services;

public interface IEmailService
{
    Task<CompleteModel> SendConfirmAccountEmail(ConfirmAccountEmail resetPassword);
    Task<CompleteModel> SendLoginLinkEmail(LoginLinkEmail loginEmail);
    Task<CompleteModel> SendResetPasswordEmail(ResetPasswordEmail resetPassword);
}
