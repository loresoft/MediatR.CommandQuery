using System;
using System.Security.Principal;
using EntityFrameworkCore.CommandQuery.Definitions;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    // ReSharper disable once UnusedTypeParameter
    public class EntityUpdateCommand<TKey, TEntity, TUpdateModel, TReadModel> : EntityModelCommand<TUpdateModel, TReadModel>
        where TEntity : class, new()
    {
        public EntityUpdateCommand(TKey id, TUpdateModel model, IPrincipal principal) : base(model, principal)
        {
            Id = id;

            var identityName = principal?.Identity?.Name;

            // ReSharper disable once InvertIf
            if (model is ITrackUpdated updatedModel)
            {
                updatedModel.Updated = DateTimeOffset.UtcNow;
                updatedModel.UpdatedBy = identityName;
            }
        }

        public TKey Id { get; }

        public TReadModel Original { get; set; }
    }
}