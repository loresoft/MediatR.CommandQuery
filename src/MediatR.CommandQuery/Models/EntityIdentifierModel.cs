using System.Diagnostics.CodeAnalysis;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Models;

/// <summary>
/// An identifier base model <c>class</c>
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <seealso cref="IHaveIdentifier{TKey}" />
public class EntityIdentifierModel<TKey> : IHaveIdentifier<TKey>
{
    /// <inheritdoc />
    [NotNull]
    public TKey Id { get; set; } = default!;
}
