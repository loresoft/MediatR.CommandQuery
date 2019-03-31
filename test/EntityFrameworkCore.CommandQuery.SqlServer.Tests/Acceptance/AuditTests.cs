using System;
using AutoMapper;
using DataGenerator;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Queries;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Acceptance
{
    public class AuditTests : DatabaseTestBase
    {
        public AuditTests(ITestOutputHelper output, DatabaseFixture databaseFixture) : base(output, databaseFixture)
        {
        }

        [Fact]
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

            var createCommand = new EntityCreateCommand<AuditCreateModel, AuditReadModel>(createModel, MockPrincipal.Default);
            var createResult = await mediator.Send(createCommand).ConfigureAwait(false);
            createResult.Should().NotBeNull();

            // Get Entity by Key
            var identifierQuery = new EntityIdentifierQuery<Guid, AuditReadModel>(createResult.Id, MockPrincipal.Default);
            var identifierResult = await mediator.Send(identifierQuery).ConfigureAwait(false);
            identifierResult.Should().NotBeNull();
            identifierResult.Username.Should().Be(createModel.Username);

            // Query Entity
            var entityQuery = new EntityQuery
            {
                Sort = new[] { new EntitySort { Name = "Updated", Direction = "Descending" } },
                Filter = new EntityFilter { Name = "Username", Value = "TEST" }
            };
            var listQuery = new EntityPagedQuery<AuditReadModel>(entityQuery, MockPrincipal.Default);

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

            var patchCommand = new EntityPatchCommand<Guid, AuditReadModel>(createResult.Id, patchModel, MockPrincipal.Default);
            var patchResult = await mediator.Send(patchCommand).ConfigureAwait(false);
            patchResult.Should().NotBeNull();
            patchResult.Content.Should().Be("Patch Update");

            // Update Entity
            var updateModel = mapper.Map<AuditUpdateModel>(patchResult);
            updateModel.Content = "Update Command";

            var updateCommand = new EntityUpdateCommand<Guid, AuditUpdateModel, AuditReadModel>(createResult.Id, updateModel, MockPrincipal.Default);
            var updateResult = await mediator.Send(updateCommand).ConfigureAwait(false);
            updateResult.Should().NotBeNull();
            updateResult.Content.Should().Be("Update Command");

            // Delete Entity
            var deleteCommand = new EntityDeleteCommand<Guid, AuditReadModel>(createResult.Id, MockPrincipal.Default);
            var deleteResult = await mediator.Send(deleteCommand).ConfigureAwait(false);
            deleteResult.Should().NotBeNull();
            deleteResult.Id.Should().Be(createResult.Id);
        }


        [Fact]
        public async Task Upsert()
        {
            var key = Guid.NewGuid();
            var mediator = ServiceProvider.GetService<IMediator>();
            mediator.Should().NotBeNull();

            var mapper = ServiceProvider.GetService<IMapper>();
            mapper.Should().NotBeNull();

            // Update Entity
            var updateModel = Generator.Default.Single<AuditUpdateModel>();
            updateModel.Username = "TEST";
            updateModel.Content = "Insert " + DateTime.Now.Ticks;

            var upsertCommandNew = new EntityUpsertCommand<Guid, AuditUpdateModel, AuditReadModel>(key, updateModel, MockPrincipal.Default);
            var upsertResultNew = await mediator.Send(upsertCommandNew).ConfigureAwait(false);
            upsertResultNew.Should().NotBeNull();

            // Get Entity by Key
            var identifierQuery = new EntityIdentifierQuery<Guid, AuditReadModel>(key, MockPrincipal.Default);
            var identifierResult = await mediator.Send(identifierQuery).ConfigureAwait(false);
            identifierResult.Should().NotBeNull();
            identifierResult.Username.Should().Be(updateModel.Username);

            // update model
            updateModel.Content = "Update " + DateTime.Now.Ticks;

            // Upsert again, should be update
            var upsertCommandUpdate = new EntityUpsertCommand<Guid, AuditUpdateModel, AuditReadModel>(key, updateModel, MockPrincipal.Default);
            var upsertResultUpdate = await mediator.Send(upsertCommandUpdate).ConfigureAwait(false);
            upsertResultUpdate.Should().NotBeNull();
            upsertResultUpdate.Content.Should().NotBe(upsertResultNew.Content);
        }

    }
}
