using MediatR.CommandQuery.Dispatcher;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority.Models;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Dispatcher;

[Collection(DatabaseCollection.CollectionName)]
public class DispatcherDataServiceTests : DatabaseTestBase
{
    public DispatcherDataServiceTests(ITestOutputHelper output, DatabaseFixture databaseFixture)
        : base(output, databaseFixture)
    {
    }

    [Fact]
    [Trait("Category", "SqlServer")]
    public async Task FullTest()
    {
        var dataService = ServiceProvider.GetService<IDispatcherDataService>();
        dataService.Should().NotBeNull();

        var generator = new Faker<PriorityCreateModel>()
            .RuleFor(p => p.Id, (faker, model) => Guid.NewGuid())
            .RuleFor(p => p.Created, (faker, model) => faker.Date.PastOffset())
            .RuleFor(p => p.CreatedBy, (faker, model) => faker.Internet.Email())
            .RuleFor(p => p.Updated, (faker, model) => faker.Date.SoonOffset())
            .RuleFor(p => p.UpdatedBy, (faker, model) => faker.Internet.Email())
            .RuleFor(p => p.Name, (faker, model) => faker.Name.JobType())
            .RuleFor(p => p.Description, (faker, model) => faker.Lorem.Sentence());

        var createModel = generator.Generate();

        var createResult = await dataService.Create<PriorityCreateModel, PriorityReadModel>(createModel);
        createResult.Should().NotBeNull();
        createResult.Name.Should().Be(createModel.Name);

        var searchResult = await dataService.Search<PriorityReadModel>(createModel.Name);
        searchResult.Should().NotBeNull();

        var selectEmptyResult = await dataService.Select<PriorityReadModel>();
        selectEmptyResult.Should().NotBeNull();

        var pageEmptyResult = await dataService.Page<PriorityReadModel>();
        pageEmptyResult.Should().NotBeNull();

        var getReadResult = await dataService.Get<Guid, PriorityReadModel>(createResult.Id);
        getReadResult.Should().NotBeNull();

        var getMultipleResult = await dataService.Get<Guid, PriorityReadModel>([createResult.Id]);
        getMultipleResult.Should().NotBeNull();


        var getUpdateResult = await dataService.Get<Guid, PriorityUpdateModel>(createResult.Id);
        getUpdateResult.Should().NotBeNull();

        getUpdateResult.Description = "This is an update";

        var updateResult = await dataService.Update<Guid, PriorityUpdateModel, PriorityReadModel>(createResult.Id, getUpdateResult);
        updateResult.Should().NotBeNull();

        var deleteResult = await dataService.Delete<Guid, PriorityReadModel>(createResult.Id);
        deleteResult.Should().NotBeNull();
    }
}
