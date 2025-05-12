using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.Identity.Models;

public class LoginCompleteModel : CompleteModel
{
    public bool IsLockedOut { get; set; }
    public bool IsNotAllowed { get; set; }
    public bool RequiresTwoFactor { get; set; }

    public static new LoginCompleteModel Success(string? message = null)
        => new() { Successful = true, Message = message };

    public static new LoginCompleteModel Fail(string? message = null)
        => new() { Successful = false, Message = message };

}
