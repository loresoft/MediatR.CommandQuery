using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace MediatR.CommandQuery.Commands
{
    public abstract class EntityIdentifiersCommand<TKey, TResponse>
        : PrincipalCommandBase<TResponse>
    {
        protected EntityIdentifiersCommand(IPrincipal principal, IEnumerable<TKey> ids)
            : base(principal)
        {
            Ids = ids.ToList();
        }

        public IReadOnlyCollection<TKey> Ids { get; }
    }
}