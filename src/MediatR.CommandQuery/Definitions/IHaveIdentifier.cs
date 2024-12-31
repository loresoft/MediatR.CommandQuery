using System.Diagnostics.CodeAnalysis;

namespace MediatR.CommandQuery.Definitions;

/// <summary>
/// An <c>interface</c> indicating the implemented type has an identifier (Primary key)
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
public interface IHaveIdentifier<TKey>
{
    /// <summary>
    /// Gets or sets the identifier for this instance.
    /// </summary>
    /// <value>
    /// The identifier for this instance.
    /// </value>
    [NotNull] TKey Id { get; set; }
}
