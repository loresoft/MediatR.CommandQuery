using System.Security.Principal;

namespace MediatR.CommandQuery.Commands
{
    public abstract class EntityIdentifierCommand<TKey, TReadModel>
        : PrincipalCommandBase<TReadModel>
    {
        protected EntityIdentifierCommand(IPrincipal principal, TKey id)
            : base(principal)
        {
            Id = id;
        }

        public TKey Id { get; }
    }
}