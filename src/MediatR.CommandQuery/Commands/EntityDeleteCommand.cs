using System.Security.Principal;

namespace MediatR.CommandQuery.Commands
{
    public class EntityDeleteCommand<TKey, TReadModel>
        : EntityIdentifierCommand<TKey, TReadModel>
    {
        public EntityDeleteCommand(IPrincipal principal, TKey id) : base(principal, id)
        {
        }
    }
}