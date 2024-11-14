using System.Collections.Generic;
using System.Text.Json.Serialization;

using MediatR.CommandQuery.Queries;

using SystemTextJsonPatch;

using Tracker.WebService.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.WebService.Domain;

[JsonSourceGenerationOptions(
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
#region Generated Attributes
[JsonSerializable(typeof(AuditReadModel))]
[JsonSerializable(typeof(IReadOnlyCollection<AuditReadModel>))]
[JsonSerializable(typeof(EntityPagedResult<AuditReadModel>))]
[JsonSerializable(typeof(AuditCreateModel))]
[JsonSerializable(typeof(AuditUpdateModel))]
[JsonSerializable(typeof(PriorityReadModel))]
[JsonSerializable(typeof(IReadOnlyCollection<PriorityReadModel>))]
[JsonSerializable(typeof(EntityPagedResult<PriorityReadModel>))]
[JsonSerializable(typeof(PriorityCreateModel))]
[JsonSerializable(typeof(PriorityUpdateModel))]
[JsonSerializable(typeof(RoleReadModel))]
[JsonSerializable(typeof(IReadOnlyCollection<RoleReadModel>))]
[JsonSerializable(typeof(EntityPagedResult<RoleReadModel>))]
[JsonSerializable(typeof(RoleCreateModel))]
[JsonSerializable(typeof(RoleUpdateModel))]
[JsonSerializable(typeof(StatusReadModel))]
[JsonSerializable(typeof(IReadOnlyCollection<StatusReadModel>))]
[JsonSerializable(typeof(EntityPagedResult<StatusReadModel>))]
[JsonSerializable(typeof(StatusCreateModel))]
[JsonSerializable(typeof(StatusUpdateModel))]
[JsonSerializable(typeof(TaskReadModel))]
[JsonSerializable(typeof(IReadOnlyCollection<TaskReadModel>))]
[JsonSerializable(typeof(EntityPagedResult<TaskReadModel>))]
[JsonSerializable(typeof(TaskCreateModel))]
[JsonSerializable(typeof(TaskUpdateModel))]
[JsonSerializable(typeof(UserLoginReadModel))]
[JsonSerializable(typeof(IReadOnlyCollection<UserLoginReadModel>))]
[JsonSerializable(typeof(EntityPagedResult<UserLoginReadModel>))]
[JsonSerializable(typeof(UserLoginCreateModel))]
[JsonSerializable(typeof(UserLoginUpdateModel))]
[JsonSerializable(typeof(UserReadModel))]
[JsonSerializable(typeof(IReadOnlyCollection<UserReadModel>))]
[JsonSerializable(typeof(EntityPagedResult<UserReadModel>))]
[JsonSerializable(typeof(UserCreateModel))]
[JsonSerializable(typeof(UserUpdateModel))]
[JsonSerializable(typeof(EntityQuery))]
[JsonSerializable(typeof(EntitySelect))]
[JsonSerializable(typeof(IJsonPatchDocument))]
#endregion
public partial class DomainJsonContext : JsonSerializerContext
{
    //test
}

