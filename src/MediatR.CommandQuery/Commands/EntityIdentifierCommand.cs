using System.Security.Principal;

namespace MediatR.CommandQuery.Commands
{
    public abstract class EntityIdentifierCommand<TKey, TResponse>
        : PrincipalCommandBase<TResponse>
    {
        protected EntityIdentifierCommand(IPrincipal principal, TKey id)
            : base(principal)
        {
            Id = id;
        }

        public TKey Id { get; }
    }
}