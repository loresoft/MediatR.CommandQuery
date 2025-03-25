using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Notifications;

/// <summary>
/// An entity change notification
/// </summary>
/// <typeparam name="TEntityModel">The type of the entity model.</typeparam>
/// <seealso cref="MediatR.INotification" />
public class EntityChangeNotification<TEntityModel> : INotification
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityChangeNotification{TEntityModel}"/> class.
    /// </summary>
    /// <param name="model">The model that has changed.</param>
    /// <param name="operation">The type of change operation.</param>
    /// <exception cref="System.ArgumentNullException">When <paramref name="model"/> is null</exception>
    public EntityChangeNotification(TEntityModel model, EntityChangeOperation operation)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model));
        Operation = operation;
    }

    /// <summary>
    /// Gets the model that has changed.
    /// </summary>
    /// <value>
    /// The model that has changed.
    /// </value>
    [JsonPropertyName("model")]
    public TEntityModel Model { get; }

    /// <summary>
    /// Gets the type of change operation.
    /// </summary>
    /// <value>
    /// The type of change operation.
    /// </value>
    [JsonPropertyName("operation")]
    [JsonConverter(typeof(JsonStringEnumConverter<EntityChangeOperation>))]
    public EntityChangeOperation Operation { get; }
}
