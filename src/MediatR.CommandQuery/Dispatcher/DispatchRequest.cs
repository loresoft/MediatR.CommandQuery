using System.Text.Json.Serialization;

using MediatR.CommandQuery.Converters;

namespace MediatR.CommandQuery.Dispatcher;

public class DispatchRequest
{
    [JsonConverter(typeof(PolymorphicConverter<IBaseRequest>))]
    public IBaseRequest Request { get; set; } = null!;
}
