using AutoMapper;

using Cosmos.Abstracts;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Results;

using Microsoft.Extensions.DependencyInjection;

using SystemTextJsonPatch;

using Task = System.Threading.Tasks.Task;

namespace MediatR.CommandQuery.Cosmos.Tests.Acceptance;

[Collection(DatabaseCollection.CollectionName)]
public class AuditTests : DatabaseTestBase
{
    public AuditTests(ITestOutputHelper output, DatabaseFixture databaseFixture)
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
        var generator = new Faker<AuditCreateModel>()
            .RuleFor(p => p.Id, (faker, model) => ObjectId.GenerateNewId().ToString())
            .RuleFor(p => p.Username, (faker, model) => faker.Internet.UserName())
            .RuleFor(p => p.Content, (faker, model) => faker.Lorem.Paragraph())
            .RuleFor(p => p.Created, (faker, model) => faker.Date.PastOffset())
            .RuleFor(p => p.CreatedBy, (faker, model) => faker.Internet.Email())
            .RuleFor(p => p.Updated, (faker, model) => faker.Date.SoonOffset())
            .RuleFor(p => p.UpdatedBy, (faker, model) => faker.Internet.Email())
            .RuleFor(p => p.Date, (faker, model) => faker.Date.Soon());

        var createModel = generator.Generate();

        var createCommand = new EntityCreateCommand<AuditCreateModel, AuditReadModel>(MockPrincipal.Default, createModel);
        var createResult = await mediator.Send(createCommand);
        createResult.Should().NotBeNull();

        // Get Entity by Key
        var key = CosmosKey.Encode(createResult.Value.Id, createResult.Value.Id);
        var identifierQuery = new EntityIdentifierQuery<string, AuditReadModel>(MockPrincipal.Default, key);
        var identifierResult = await mediator.Send(identifierQuery);
        identifierResult.Should().NotBeNull();
        identifierResult.Value.Username.Should().Be(createModel.Username);

        // Query Entity
        var entityQuery = new EntityQuery
        {
            Sort = new List<EntitySort> { new EntitySort { Name = "Updated", Direction = "Descending" } },
            Filter = new EntityFilter { Name = "Username", Value = "TEST" }
        };
        var listQuery = new EntityPagedQuery<AuditReadModel>(MockPrincipal.Default, entityQuery);

        var listResult = await mediator.Send(listQuery);
        listResult.Should().NotBeNull();

        // Patch Entity
        var patchModel = new JsonPatchDocument();
        patchModel.Replace("/Content", "Patch Update");

        var patchCommand = new EntityPatchCommand<string, AuditReadModel>(MockPrincipal.Default, key, patchModel);
        var patchResult = await mediator.Send(patchCommand);
        patchResult.Should().NotBeNull();
        patchResult.Value.Content.Should().Be("Patch Update");

        // Update Entity
        var updateModel = mapper.Map<AuditUpdateModel>(patchResult);
        updateModel.Content = "Update Command";

        var updateCommand = new EntityUpdateCommand<string, AuditUpdateModel, AuditReadModel>(MockPrincipal.Default, key, updateModel);
        var updateResult = await mediator.Send(updateCommand);
        updateResult.Should().NotBeNull();
        updateResult.Value.Content.Should().Be("Update Command");

        // Delete Entity
        var deleteCommand = new EntityDeleteCommand<string, AuditReadModel>(MockPrincipal.Default, key);
        var deleteResult = await mediator.Send(deleteCommand);
        deleteResult.Should().NotBeNull();
        deleteResult.Value.Id.Should().Be(createResult.Value.Id);
    }


    [Fact]
    [Trait("Category", "Cosmos")]
    public async Task Upsert()
    {
        var id = ObjectId.GenerateNewId().ToString();
        var key = CosmosKey.Encode(id, id);

        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var handler = ServiceProvider.GetService<IRequestHandler<EntityIdentifierQuery<string, AuditReadModel>, IResult<AuditReadModel>>>();

        // Update Entity
        var generator = new Faker<AuditUpdateModel>()
            .RuleFor(p => p.Username, (faker, model) => faker.Internet.UserName())
            .RuleFor(p => p.Content, (faker, model) => faker.Lorem.Paragraph())
            .RuleFor(p => p.Updated, (faker, model) => faker.Date.SoonOffset())
            .RuleFor(p => p.UpdatedBy, (faker, model) => faker.Internet.Email())
            .RuleFor(p => p.Date, (faker, model) => faker.Date.Soon());

        var updateModel = generator.Generate();

        var upsertCommandNew = new EntityUpsertCommand<string, AuditUpdateModel, AuditReadModel>(MockPrincipal.Default, key, updateModel);
        var upsertResultNew = await mediator.Send(upsertCommandNew);
        upsertResultNew.Should().NotBeNull();

        // Get Entity by Key
        var identifierQuery = new EntityIdentifierQuery<string, AuditReadModel>(MockPrincipal.Default, key);
        var identifierResult = await mediator.Send(identifierQuery);
        identifierResult.Should().NotBeNull();
        identifierResult.Value.Username.Should().Be(updateModel.Username);

        // update model
        updateModel.Content = "Update " + DateTime.Now.Ticks;

        // Upsert again, should be update
        var upsertCommandUpdate = new EntityUpsertCommand<string, AuditUpdateModel, AuditReadModel>(MockPrincipal.Default, key, updateModel);
        var upsertResultUpdate = await mediator.Send(upsertCommandUpdate);
        upsertResultUpdate.Should().NotBeNull();
        upsertResultUpdate.Value.Content.Should().NotBe(upsertResultNew.Value.Content);
    }

}
