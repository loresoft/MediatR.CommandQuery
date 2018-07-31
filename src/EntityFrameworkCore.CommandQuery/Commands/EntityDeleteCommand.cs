using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    // ReSharper disable once UnusedTypeParameter
    public class EntityDeleteCommand<TKey, TEntity, TReadModel>
        : EntityIdentifierCommand<TKey, TReadModel>
        where TEntity : class, new()
    {
        public EntityDeleteCommand(TKey id, IPrincipal principal) : base(id, principal)
        {
        }
    }
}