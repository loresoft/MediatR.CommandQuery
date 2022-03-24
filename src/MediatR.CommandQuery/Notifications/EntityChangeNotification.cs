using System;

namespace MediatR.CommandQuery.Notifications;

public class EntityChangeNotification<TEntityModel> : INotification
{
    public EntityChangeNotification(TEntityModel model, EntityChangeOperation operation)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model));
        Operation = operation;
    }

    public TEntityModel Model { get; }

    public EntityChangeOperation Operation { get; }
}
