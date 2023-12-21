using System;
using System.Collections.Generic;

using AutoMapper;

using Bogus;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit.Models;
using MediatR.CommandQuery.Queries;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.DependencyInjection;

using Xunit.Abstractions;

using Task = System.Threading.Tasks.Task;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Acceptance;

[Collection(DatabaseCollection.CollectionName)]
public class AuditTests : DatabaseTestBase
{
    public AuditTests(ITestOutputHelper output, DatabaseFixture databaseFixture) : base(output, databaseFixture)
    {
    }

    [Fact]
    [Trait("Category", "SqlServer")]
    public async Task FullTest()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        // Create Entity
        var generator = new Faker<AuditCreateModel>()
            .RuleFor(p => p.Id, (faker, model) => Guid.NewGuid())
            .RuleFor(p => p.Created, (faker, model) => faker.Date.PastOffset())
            .RuleFor(p => p.CreatedBy, (faker, model) => faker.Internet.Email())
            .RuleFor(p => p.Updated, (faker, model) => faker.Date.SoonOffset())
            .RuleFor(p => p.UpdatedBy, (faker, model) => faker.Internet.Email())
            .RuleFor(p => p.Date, (faker, model) => faker.Date.Soon());

        var createModel = generator.Generate();
        createModel.Username = "TEST";
        createModel.Content = "Test " + DateTime.Now.Ticks;

        var createCommand = new EntityCreateCommand<AuditCreateModel, AuditReadModel>(MockPrincipal.Default, createModel);
        var createResult = await mediator.Send(createCommand);
        createResult.Should().NotBeNull();

        // Get Entity by Key
        var identifierQuery = new EntityIdentifierQuery<Guid, AuditReadModel>(MockPrincipal.Default, createResult.Id);
        var identifierResult = await mediator.Send(identifierQuery);
        identifierResult.Should().NotBeNull();
        identifierResult.Username.Should().Be(createModel.Username);

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
        var patchModel = new JsonPatchDocument<Audit>();
        patchModel.Operations.Add(new Operation<Audit>
        {
            op = "replace",
            path = "/Content",
            value = "Patch Update"
        });

        var patchCommand = new EntityPatchCommand<Guid, AuditReadModel>(MockPrincipal.Default, createResult.Id, patchModel);
        var patchResult = await mediator.Send(patchCommand);
        patchResult.Should().NotBeNull();
        patchResult.Content.Should().Be("Patch Update");

        // Update Entity
        var updateModel = mapper.Map<AuditUpdateModel>(patchResult);
        updateModel.Content = "Update Command";

        var updateCommand = new EntityUpdateCommand<Guid, AuditUpdateModel, AuditReadModel>(MockPrincipal.Default, createResult.Id, updateModel);
        var updateResult = await mediator.Send(updateCommand);
        updateResult.Should().NotBeNull();
        updateResult.Content.Should().Be("Update Command");

        // Delete Entity
        var deleteCommand = new EntityDeleteCommand<Guid, AuditReadModel>(MockPrincipal.Default, createResult.Id);
        var deleteResult = await mediator.Send(deleteCommand);
        deleteResult.Should().NotBeNull();
        deleteResult.Id.Should().Be(createResult.Id);
    }


    [Fact]
    [Trait("Category", "SqlServer")]
    public async Task Upsert()
    {
        var key = Guid.NewGuid();
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        // Update Entity
        var generator = new Faker<AuditUpdateModel>()
            .RuleFor(p => p.Updated, (faker, model) => faker.Date.SoonOffset())
            .RuleFor(p => p.UpdatedBy, (faker, model) => faker.Internet.Email())
            .RuleFor(p => p.Date, (faker, model) => faker.Date.Soon());

        var updateModel = generator.Generate();
        updateModel.Username = "TEST";
        updateModel.Content = "Insert " + DateTime.Now.Ticks;

        var upsertCommandNew = new EntityUpsertCommand<Guid, AuditUpdateModel, AuditReadModel>(MockPrincipal.Default, key, updateModel);
        var upsertResultNew = await mediator.Send(upsertCommandNew);
        upsertResultNew.Should().NotBeNull();

        // Get Entity by Key
        var identifierQuery = new EntityIdentifierQuery<Guid, AuditReadModel>(MockPrincipal.Default, key);
        var identifierResult = await mediator.Send(identifierQuery);
        identifierResult.Should().NotBeNull();
        identifierResult.Username.Should().Be(updateModel.Username);

        // update model
        updateModel.Content = "Update " + DateTime.Now.Ticks;

        // Upsert again, should be update
        var upsertCommandUpdate = new EntityUpsertCommand<Guid, AuditUpdateModel, AuditReadModel>(MockPrincipal.Default, key, updateModel);
        var upsertResultUpdate = await mediator.Send(upsertCommandUpdate);
        upsertResultUpdate.Should().NotBeNull();
        upsertResultUpdate.Content.Should().NotBe(upsertResultNew.Content);
    }

}
