using System;
using System.Collections.Generic;

using AutoMapper;

using Cosmos.Abstracts;

using DataGenerator;

using FluentAssertions;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Cosmos.Tests.Data.Entities;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;
using MediatR.CommandQuery.Queries;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.DependencyInjection;

using Xunit;
using Xunit.Abstractions;

using Task = System.Threading.Tasks.Task;

namespace MediatR.CommandQuery.Cosmos.Tests.Acceptance;

public class AuditTests : DatabaseTestBase
{
    public AuditTests(ITestOutputHelper output, DatabaseFixture databaseFixture) : base(output, databaseFixture)
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
        var createModel = Generator.Default.Single<AuditCreateModel>();
        createModel.Username = "TEST";
        createModel.Content = "Test " + DateTime.Now.Ticks;

        var createCommand = new EntityCreateCommand<AuditCreateModel, AuditReadModel>(MockPrincipal.Default, createModel);
        var createResult = await mediator.Send(createCommand).ConfigureAwait(false);
        createResult.Should().NotBeNull();

        // Get Entity by Key
        var key = CosmosKey.Encode(createResult.Id, createResult.Id);
        var identifierQuery = new EntityIdentifierQuery<string, AuditReadModel>(MockPrincipal.Default, key);
        var identifierResult = await mediator.Send(identifierQuery).ConfigureAwait(false);
        identifierResult.Should().NotBeNull();
        identifierResult.Username.Should().Be(createModel.Username);

        // Query Entity
        var entityQuery = new EntityQuery
        {
            Sort = new List<EntitySort> { new EntitySort { Name = "Updated", Direction = "Descending" } },
            Filter = new EntityFilter { Name = "Username", Value = "TEST" }
        };
        var listQuery = new EntityPagedQuery<AuditReadModel>(MockPrincipal.Default, entityQuery);

        var listResult = await mediator.Send(listQuery).ConfigureAwait(false);
        listResult.Should().NotBeNull();

        // Patch Entity
        var patchModel = new JsonPatchDocument<Audit>();
        patchModel.Operations.Add(new Operation<Audit>
        {
            op = "replace",
            path = "/Content",
            value = "Patch Update"
        });


        var patchCommand = new EntityPatchCommand<string, AuditReadModel>(MockPrincipal.Default, key, patchModel);
        var patchResult = await mediator.Send(patchCommand).ConfigureAwait(false);
        patchResult.Should().NotBeNull();
        patchResult.Content.Should().Be("Patch Update");

        // Update Entity
        var updateModel = mapper.Map<AuditUpdateModel>(patchResult);
        updateModel.Content = "Update Command";

        var updateCommand = new EntityUpdateCommand<string, AuditUpdateModel, AuditReadModel>(MockPrincipal.Default, key, updateModel);
        var updateResult = await mediator.Send(updateCommand).ConfigureAwait(false);
        updateResult.Should().NotBeNull();
        updateResult.Content.Should().Be("Update Command");

        // Delete Entity
        var deleteCommand = new EntityDeleteCommand<string, AuditReadModel>(MockPrincipal.Default, key);
        var deleteResult = await mediator.Send(deleteCommand).ConfigureAwait(false);
        deleteResult.Should().NotBeNull();
        deleteResult.Id.Should().Be(createResult.Id);
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

        // Update Entity
        var updateModel = Generator.Default.Single<AuditUpdateModel>();
        updateModel.Username = "TEST";
        updateModel.Content = "Insert " + DateTime.Now.Ticks;

        var upsertCommandNew = new EntityUpsertCommand<string, AuditUpdateModel, AuditReadModel>(MockPrincipal.Default, key, updateModel);
        var upsertResultNew = await mediator.Send(upsertCommandNew).ConfigureAwait(false);
        upsertResultNew.Should().NotBeNull();

        // Get Entity by Key
        var identifierQuery = new EntityIdentifierQuery<string, AuditReadModel>(MockPrincipal.Default, key);
        var identifierResult = await mediator.Send(identifierQuery).ConfigureAwait(false);
        identifierResult.Should().NotBeNull();
        identifierResult.Username.Should().Be(updateModel.Username);

        // update model
        updateModel.Content = "Update " + DateTime.Now.Ticks;

        // Upsert again, should be update
        var upsertCommandUpdate = new EntityUpsertCommand<string, AuditUpdateModel, AuditReadModel>(MockPrincipal.Default, key, updateModel);
        var upsertResultUpdate = await mediator.Send(upsertCommandUpdate).ConfigureAwait(false);
        upsertResultUpdate.Should().NotBeNull();
        upsertResultUpdate.Content.Should().NotBe(upsertResultNew.Content);
    }

}
