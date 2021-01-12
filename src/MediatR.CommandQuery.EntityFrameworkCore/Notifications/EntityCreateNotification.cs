namespace MediatR.CommandQuery.EntityFrameworkCore.Notifications
{
    public class EntityChangeNotification<TEntityModel> : INotification
    {
        public EntityChangeNotification(TEntityModel model, EntityChangeOperation operation)
        {
            Model = model;
            Operation = operation;
        }

        public TEntityModel Model { get; }
        
        public EntityChangeOperation Operation { get; }
    }
}
