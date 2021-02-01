using System.Security.Principal;

namespace MediatR.CommandQuery.Commands
{
    public class EntityUpdateCommand<TKey, TUpdateModel, TReadModel>
        : EntityModelCommand<TUpdateModel, TReadModel>
    {
        public EntityUpdateCommand(IPrincipal principal, TKey id, TUpdateModel model) : base(principal, model)
        {
            Id = id;
        }

        public TKey Id { get; }

        public override string ToString()
        {
            return $"Entity Update Command; Model: {typeof(TUpdateModel).Name}; Id: {Id}; {base.ToString()}";
        }
    }
}