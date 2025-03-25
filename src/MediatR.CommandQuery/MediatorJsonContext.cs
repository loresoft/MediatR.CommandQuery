using System.Text.Json.Serialization;

using MediatR.CommandQuery.Dispatcher;
using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery;

[JsonSerializable(typeof(DispatchRequest))]
[JsonSerializable(typeof(ProblemDetails))]
public partial class MediatorJsonContext : JsonSerializerContext;
