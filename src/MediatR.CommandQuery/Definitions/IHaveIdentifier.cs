using System.Diagnostics.CodeAnalysis;

namespace MediatR.CommandQuery.Definitions;

public interface IHaveIdentifier<TKey>
{
    [NotNull] TKey Id { get; set; }
}
