using AutoMapper;

using Cosmos.Abstracts;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Cosmos.Tests.Constants;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.DependencyInjection;

using SystemTextJsonPatch;
using SystemTextJsonPatch.Operations;

using Task = System.Threading.Tasks.Task;

namespace MediatR.CommandQuery.Cosmos.Tests.Acceptance;

[Collection(DatabaseCollection.CollectionName)]
public class TaskTests : DatabaseTestBase
{
    public TaskTests(ITestOutputHelper output, DatabaseFixture databaseFixture)
        : base(output, databaseFixture)
    {
    }

    [Fact]
    [Trait("Category", "Cosmos")]
    public async Task FullTest()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        // Create Entity
        var generator = new Faker<TaskCreateModel>()
            .RuleFor(t => t.TenantId, f => TenantConstants.Test.Id)
            .RuleFor(t => t.StatusId, f => StatusConstants.NotStarted.Id)
            .RuleFor(t => t.PriorityId, f => PriorityConstants.Normal.Id)
            .RuleFor(t => t.Title, f => f.Lorem.Word())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence());

        var createModel = generator.Generate();

        var createCommand = new EntityCreateCommand<TaskCreateModel, TaskReadModel>(MockPrincipal.Default, createModel);
        var createResult = await mediator.Send(createCommand);
        createResult.Should().NotBeNull();

        // Get Entity by Key
        var cosmosKey = CosmosKey.Encode(createResult.Id, createResult.TenantId);
        var identifierQuery = new EntityIdentifierQuery<string, TaskReadModel>(MockPrincipal.Default, cosmosKey);
        var identifierResult = await mediator.Send(identifierQuery);
        identifierResult.Should().NotBeNull();
        identifierResult.Title.Should().Be(createModel.Title);

        // Query Entity
        var entityQuery = new EntityQuery
        {
            Sort = new List<EntitySort> { new EntitySort { Name = "Updated", Direction = "Descending" } },
            Filter = new EntityFilter { Name = "StatusId", Value = StatusConstants.NotStarted.Id }
        };
        var listQuery = new EntityPagedQuery<TaskReadModel>(MockPrincipal.Default, entityQuery);

        var listResult = await mediator.Send(listQuery);
        listResult.Should().NotBeNull();

        // Patch Entity
        var patchModel = new JsonPatchDocument();
        patchModel.Operations.Add(new Operation
        {
            Op = "replace",
            Path = "/Title",
            Value = "Patch Update"
        });

        var patchCommand = new EntityPatchCommand<string, TaskReadModel>(MockPrincipal.Default, cosmosKey, patchModel);
        var patchResult = await mediator.Send(patchCommand);
        patchResult.Should().NotBeNull();
        patchResult.Title.Should().Be("Patch Update");

        // Update Entity
        var updateModel = mapper.Map<TaskUpdateModel>(patchResult);
        updateModel.Title = "Update Command";

        var updateCommand = new EntityUpdateCommand<string, TaskUpdateModel, TaskReadModel>(MockPrincipal.Default, cosmosKey, updateModel);
        var updateResult = await mediator.Send(updateCommand);
        updateResult.Should().NotBeNull();
        updateResult.Title.Should().Be("Update Command");

        // Delete Entity
        var deleteCommand = new EntityDeleteCommand<string, TaskReadModel>(MockPrincipal.Default, cosmosKey);
        var deleteResult = await mediator.Send(deleteCommand);
        deleteResult.Should().NotBeNull();
        deleteResult.Id.Should().Be(createResult.Id);
    }

    [Fact]
    [Trait("Category", "Cosmos")]
    public async Task Upsert()
    {
        var cosmosKey = CosmosKey.Encode(ObjectId.GenerateNewId().ToString(), Constants.TenantConstants.Test.Id);

        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        // Update Entity
        var generator = new Faker<TaskUpdateModel>()
            .RuleFor(t => t.TenantId, f => TenantConstants.Test.Id)
            .RuleFor(t => t.StatusId, f => StatusConstants.NotStarted.Id)
            .RuleFor(t => t.PriorityId, f => PriorityConstants.Normal.Id)
            .RuleFor(t => t.Title, f => f.Lorem.Word())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence());

        var updateModel = generator.Generate();

        var upsertCommandNew = new EntityUpsertCommand<string, TaskUpdateModel, TaskReadModel>(MockPrincipal.Default, cosmosKey, updateModel);
        var upsertResultNew = await mediator.Send(upsertCommandNew);
        upsertResultNew.Should().NotBeNull();

        // Get Entity by Key
        var identifierQuery = new EntityIdentifierQuery<string, TaskReadModel>(MockPrincipal.Default, cosmosKey);
        var identifierResult = await mediator.Send(identifierQuery);
        identifierResult.Should().NotBeNull();
        identifierResult.Title.Should().Be(updateModel.Title);

        // update model
        updateModel.Description = "Update " + DateTime.Now.Ticks;

        // Upsert again, should be update
        var upsertCommandUpdate = new EntityUpsertCommand<string, TaskUpdateModel, TaskReadModel>(MockPrincipal.Default, cosmosKey, updateModel);
        var upsertResultUpdate = await mediator.Send(upsertCommandUpdate);
        upsertResultUpdate.Should().NotBeNull();
        upsertResultUpdate.Description.Should().NotBe(upsertResultNew.Description);
    }

    [Fact]
    [Trait("Category", "Cosmos")]
    public async Task TenantDoesNotMatch()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        // Create Entity
        var generator = new Faker<TaskCreateModel>()
            .RuleFor(t => t.TenantId, f => Guid.NewGuid().ToString())
            .RuleFor(t => t.StatusId, f => StatusConstants.NotStarted.Id)
            .RuleFor(t => t.PriorityId, f => PriorityConstants.Normal.Id)
            .RuleFor(t => t.Title, f => f.Lorem.Word())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence());

        var createModel = generator.Generate();

        var createCommand = new EntityCreateCommand<TaskCreateModel, TaskReadModel>(MockPrincipal.Default, createModel);
        await Assert.ThrowsAsync<DomainException>(() => mediator.Send(createCommand));
    }

    [Fact]
    [Trait("Category", "Cosmos")]
    public async Task TenantSetDefault()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        // Create Entity
        var generator = new Faker<TaskCreateModel>()
            .RuleFor(t => t.StatusId, f => StatusConstants.NotStarted.Id)
            .RuleFor(t => t.PriorityId, f => PriorityConstants.Normal.Id)
            .RuleFor(t => t.Title, f => f.Lorem.Word())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence());

        var createModel = generator.Generate();

        var createCommand = new EntityCreateCommand<TaskCreateModel, TaskReadModel>(MockPrincipal.Default, createModel);
        var createResult = await mediator.Send(createCommand);

        createResult.Should().NotBeNull();
        createResult.TenantId.Should().Be(TenantConstants.Test.Id);
    }

    [Fact]
    [Trait("Category", "Cosmos")]
    public async Task EntityPageQuery()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var filter = new EntityFilter { Name = "StatusId", Value = StatusConstants.NotStarted.Id };
        var entityQuery = new EntityQuery { Filter = filter };
        var pagedQuery = new EntityPagedQuery<TaskReadModel>(MockPrincipal.Default, entityQuery);

        var selectResult = await mediator.Send(pagedQuery);
        selectResult.Should().NotBeNull();
    }

    [Fact]
    [Trait("Category", "Cosmos")]
    public async Task EntitySelectQuery()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var filter = new EntityFilter { Name = "StatusId", Value = StatusConstants.NotStarted.Id };
        var select = new EntitySelect(filter);
        var selectQuery = new EntitySelectQuery<TaskReadModel>(MockPrincipal.Default, select);

        var selectResult = await mediator.Send(selectQuery);
        selectResult.Should().NotBeNull();
    }

    [Fact]
    [Trait("Category", "Cosmos")]
    public async Task EntitySelectQueryDelete()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var filter = new EntityFilter { Name = "IsDeleted", Value = true };
        var select = new EntitySelect(filter);
        var selectQuery = new EntitySelectQuery<TaskReadModel>(MockPrincipal.Default, select);

        var selectResult = await mediator.Send(selectQuery);
        selectResult.Should().NotBeNull();
    }

    [Fact]
    [Trait("Category", "Cosmos")]
    public async Task EntitySelectQueryDeleteNested()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var filter = new EntityFilter
        {
            Filters = new List<EntityFilter>
            {
                new EntityFilter {Name = "IsDeleted", Value = true},
                new EntityFilter { Name = "StatusId", Value = StatusConstants.NotStarted.Id }
            }
        };


        var select = new EntitySelect(filter);
        var selectQuery = new EntitySelectQuery<TaskReadModel>(MockPrincipal.Default, select);

        var selectResult = await mediator.Send(selectQuery);
        selectResult.Should().NotBeNull();
    }
}
