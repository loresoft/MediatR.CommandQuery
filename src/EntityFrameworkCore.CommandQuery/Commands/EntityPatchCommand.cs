using System;
using System.Security.Principal;
using Microsoft.AspNetCore.JsonPatch;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public class EntityPatchCommand<TKey, TEntity, TReadModel>
        : EntityIdentifierCommand<TKey, TReadModel>
        where TEntity : class, new()
    {
        public EntityPatchCommand(TKey id, JsonPatchDocument<TEntity> patch, IPrincipal principal) : base(id, principal)
        {
            if (patch == null)
                throw new ArgumentNullException(nameof(patch));

            Patch = patch;
        }

        public JsonPatchDocument<TEntity> Patch { get; }

        public TReadModel Original { get; set; }
    }
}