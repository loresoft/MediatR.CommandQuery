using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public class EntityDeleteCommand<TKey, TReadModel>
        : EntityIdentifierCommand<TKey, TReadModel>
    {
        public EntityDeleteCommand(TKey id, IPrincipal principal) : base(id, principal)
        {
        }
    }
}