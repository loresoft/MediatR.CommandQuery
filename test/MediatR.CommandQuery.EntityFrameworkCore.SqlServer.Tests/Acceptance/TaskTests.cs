using System;
using AutoMapper;
using DataGenerator;
using FluentAssertions;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Constants;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Task.Models;
using MediatR.CommandQuery.Queries;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Acceptance
{
    public class TaskTests : DatabaseTestBase
    {
        public TaskTests(ITestOutputHelper output, DatabaseFixture databaseFixture)
            : base(output, databaseFixture)
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
            var createModel = Generator.Default.Single<TaskCreateModel>();
            createModel.Title = "Testing";
            createModel.Description = "Test " + DateTime.Now.Ticks;
            createModel.StatusId = StatusConstants.NotStarted;
            createModel.TenantId = TenantConstants.Test;

            var createCommand = new EntityCreateCommand<TaskCreateModel, TaskReadModel>(MockPrincipal.Default, createModel);
            var createResult = await mediator.Send(createCommand).ConfigureAwait(false);
            createResult.Should().NotBeNull();

            // Get Entity by Key
            var identifierQuery = new EntityIdentifierQuery<Guid, TaskReadModel>(MockPrincipal.Default, createResult.Id);
            var identifierResult = await mediator.Send(identifierQuery).ConfigureAwait(false);
            identifierResult.Should().NotBeNull();
            identifierResult.Title.Should().Be(createModel.Title);

            // Query Entity
            var entityQuery = new EntityQuery
            {
                Sort = new[] { new EntitySort { Name = "Updated", Direction = "Descending" } },
                Filter = new EntityFilter { Name = "StatusId", Value = StatusConstants.NotStarted }
            };
            var listQuery = new EntityPagedQuery<TaskReadModel>(MockPrincipal.Default, entityQuery);

            var listResult = await mediator.Send(listQuery).ConfigureAwait(false);
            listResult.Should().NotBeNull();

            // Patch Entity
            var patchModel = new JsonPatchDocument<Task>();
            patchModel.Operations.Add(new Operation<Task>
            {
                op = "replace",
                path = "/Title",
                value = "Patch Update"
            });

            var patchCommand = new EntityPatchCommand<Guid, TaskReadModel>(MockPrincipal.Default, createResult.Id, patchModel);
            var patchResult = await mediator.Send(patchCommand).ConfigureAwait(false);
            patchResult.Should().NotBeNull();
            patchResult.Title.Should().Be("Patch Update");

            // Update Entity
            var updateModel = mapper.Map<TaskUpdateModel>(patchResult);
            updateModel.Title = "Update Command";

            var updateCommand = new EntityUpdateCommand<Guid, TaskUpdateModel, TaskReadModel>(MockPrincipal.Default, createResult.Id, updateModel);
            var updateResult = await mediator.Send(updateCommand).ConfigureAwait(false);
            updateResult.Should().NotBeNull();
            updateResult.Title.Should().Be("Update Command");

            // Delete Entity
            var deleteCommand = new EntityDeleteCommand<Guid, TaskReadModel>(MockPrincipal.Default, createResult.Id);
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
            var updateModel = Generator.Default.Single<TaskUpdateModel>();
            updateModel.Title = "Upsert Test";
            updateModel.Description = "Insert " + DateTime.Now.Ticks;
            updateModel.StatusId = StatusConstants.NotStarted;
            updateModel.TenantId = TenantConstants.Test;

            var upsertCommandNew = new EntityUpsertCommand<Guid, TaskUpdateModel, TaskReadModel>(MockPrincipal.Default, key, updateModel);
            var upsertResultNew = await mediator.Send(upsertCommandNew).ConfigureAwait(false);
            upsertResultNew.Should().NotBeNull();

            // Get Entity by Key
            var identifierQuery = new EntityIdentifierQuery<Guid, TaskReadModel>(MockPrincipal.Default, key);
            var identifierResult = await mediator.Send(identifierQuery).ConfigureAwait(false);
            identifierResult.Should().NotBeNull();
            identifierResult.Title.Should().Be(updateModel.Title);

            // update model
            updateModel.Description = "Update " + DateTime.Now.Ticks;

            // Upsert again, should be update
            var upsertCommandUpdate = new EntityUpsertCommand<Guid, TaskUpdateModel, TaskReadModel>(MockPrincipal.Default, key, updateModel);
            var upsertResultUpdate = await mediator.Send(upsertCommandUpdate).ConfigureAwait(false);
            upsertResultUpdate.Should().NotBeNull();
            upsertResultUpdate.Description.Should().NotBe(upsertResultNew.Description);
        }

        [Fact]
        public async Task TenantDoesNotMatch()
        {
            var mediator = ServiceProvider.GetService<IMediator>();
            mediator.Should().NotBeNull();

            var mapper = ServiceProvider.GetService<IMapper>();
            mapper.Should().NotBeNull();

            // Create Entity
            var createModel = Generator.Default.Single<TaskCreateModel>();
            createModel.Title = "Testing";
            createModel.Description = "Test " + DateTime.Now.Ticks;
            createModel.StatusId = StatusConstants.NotStarted;
            createModel.TenantId = Guid.NewGuid();

            var createCommand = new EntityCreateCommand<TaskCreateModel, TaskReadModel>(MockPrincipal.Default, createModel);
            await Assert.ThrowsAsync<DomainException>(() => mediator.Send(createCommand));
        }

        [Fact]
        public async Task TenantSetDefault()
        {
            var mediator = ServiceProvider.GetService<IMediator>();
            mediator.Should().NotBeNull();

            var mapper = ServiceProvider.GetService<IMapper>();
            mapper.Should().NotBeNull();

            // Create Entity
            var createModel = Generator.Default.Single<TaskCreateModel>();
            createModel.Title = "Testing";
            createModel.Description = "Test " + DateTime.Now.Ticks;
            createModel.StatusId = StatusConstants.NotStarted;
            createModel.TenantId = Guid.Empty;

            var createCommand = new EntityCreateCommand<TaskCreateModel, TaskReadModel>(MockPrincipal.Default, createModel);
            var createResult = await mediator.Send(createCommand).ConfigureAwait(false);

            createResult.Should().NotBeNull();
            createResult.TenantId.Should().Be(TenantConstants.Test);
        }

        [Fact]
        public async Task EntitySelectQuery()
        {
            var mediator = ServiceProvider.GetService<IMediator>();
            mediator.Should().NotBeNull();

            var mapper = ServiceProvider.GetService<IMapper>();
            mapper.Should().NotBeNull();

            var filter = new EntityFilter { Name = "StatusId", Value = StatusConstants.NotStarted };
            var select = new EntitySelect(filter);
            var selectQuery = new EntitySelectQuery<TaskReadModel>(MockPrincipal.Default, select);

            var selectResult = await mediator.Send(selectQuery).ConfigureAwait(false);
            selectResult.Should().NotBeNull();
        }

    }
}
