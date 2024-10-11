using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MediatR.CommandQuery.Commands;

public record EntityUpsertCommand<TKey, TUpdateModel, TReadModel>
    : EntityModelCommand<TUpdateModel, TReadModel>
{
    public EntityUpsertCommand(ClaimsPrincipal? principal, [NotNull] TKey id, TUpdateModel model) : base(principal, model)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));

        Id = id;
    }

    [NotNull]
    public TKey Id { get; }
}
