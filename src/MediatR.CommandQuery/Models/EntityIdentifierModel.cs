using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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
    [JsonPropertyName("id")]
    [JsonPropertyOrder(-9999)]
    public TKey Id { get; set; } = default!;
}
