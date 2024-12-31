using System.Security.Claims;
using System.Text.Json.Serialization;

using MediatR.CommandQuery.Converters;

namespace MediatR.CommandQuery.Commands;

/// <summary>
/// A base command type using the specified <see cref="ClaimsPrincipal"/>.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <seealso cref="MediatR.IRequest&lt;TResponse&gt;" />
/// <seealso cref="MediatR.IBaseRequest" />
public abstract record PrincipalCommandBase<TResponse> : IRequest<TResponse>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PrincipalCommandBase{TResponse}"/> class.
    /// </summary>
    /// <param name="principal">the <see cref="ClaimsPrincipal"/> this command is run for</param>
    protected PrincipalCommandBase(ClaimsPrincipal? principal)
    {
        Principal = principal;

        Activated = DateTimeOffset.UtcNow;
        ActivatedBy = principal?.Identity?.Name ?? "system";
    }

    /// <summary>
    /// Gets the <see cref="ClaimsPrincipal"/> this command is run for.
    /// </summary>
    /// <value>
    /// The <see cref="ClaimsPrincipal"/> this command is run for.
    /// </value>
    [JsonConverter(typeof(ClaimsPrincipalConverter))]
    public ClaimsPrincipal? Principal { get; }

    /// <summary>
    /// Gets the timestamp this command was activated on.
    /// </summary>
    /// <value>
    /// The timestamp this command was activated on.
    /// </value>
    public DateTimeOffset Activated { get; }

    /// <summary>
    /// Gets the user name this command was activated by.  Extracted from the specified <see cref="Principal"/>.
    /// </summary>
    /// <value>
    /// The user name this command was activated by
    /// </value>
    /// <see cref="ClaimsIdentity.Name"/>
    public string? ActivatedBy { get; }
}
