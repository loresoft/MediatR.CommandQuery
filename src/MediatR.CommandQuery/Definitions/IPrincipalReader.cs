using System.Security.Principal;

namespace MediatR.CommandQuery.Definitions;

/// <summary>
/// Definition for reading <see cref="IPrincipal"/> claims
/// </summary>
public interface IPrincipalReader
{
    /// <summary>
    /// Gets the identifier claim from the specified <paramref name="principal"/>.
    /// </summary>
    /// <param name="principal">The principal to read the claim from.</param>
    /// <returns>The identifier claim if found; otherwise <c>null</c>.</returns>
    string? GetIdentifier(IPrincipal? principal);

    /// <summary>
    /// Gets the name claim from the specified <paramref name="principal"/>.
    /// </summary>
    /// <param name="principal">The principal to read the claim from.</param>
    /// <returns>The name claim if found; otherwise <c>null</c>.</returns>
    string? GetName(IPrincipal? principal);

    /// <summary>
    /// Gets the email claim from the specified <paramref name="principal"/>.
    /// </summary>
    /// <param name="principal">The principal to read the claim from.</param>
    /// <returns>The email claim if found; otherwise <c>null</c>.</returns>
    string? GetEmail(IPrincipal? principal);

    /// <summary>
    /// Gets the object identifier claim from the specified <paramref name="principal"/>.
    /// </summary>
    /// <param name="principal">The principal to read the claim from.</param>
    /// <returns>The object identifier claim if found; otherwise <c>null</c>.</returns>
    Guid? GetObjectId(IPrincipal? principal);
}
