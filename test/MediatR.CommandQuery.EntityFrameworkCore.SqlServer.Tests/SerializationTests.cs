using System.Text.Json;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Converters;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests;

public class SerializationTests
{
    [Fact]
    public void EntityCreateCommandSerialize()
    {
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

        var options = SerializerOptions();

        var json = JsonSerializer.Serialize(createCommand, options);
        json.Should().NotBeNullOrEmpty();


        var deserializeCommand = JsonSerializer.Deserialize<EntityCreateCommand<AuditCreateModel, AuditReadModel>>(json);
        deserializeCommand.Should().NotBeNull();
    }


    [Fact]
    public void PolymorphicConverterTest()
    {
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

        var options = SerializerOptions();

        var json = JsonSerializer.Serialize<IBaseRequest>(createCommand, options);
        json.Should().NotBeNullOrEmpty();

        var deserializeCommand = JsonSerializer.Deserialize(json, typeof(IBaseRequest), options);
        deserializeCommand.Should().NotBeNull();
        deserializeCommand.Should().BeAssignableTo<IBaseRequest>();
    }

    [Fact]
    public void PolymorphicConverterNullTest()
    {
        EntityCreateCommand<AuditCreateModel, AuditReadModel> createCommand = null;

        var options = SerializerOptions();

        var json = JsonSerializer.Serialize<IBaseRequest>(createCommand, options);
        json.Should().NotBeNullOrEmpty();

        var deserializeCommand = JsonSerializer.Deserialize(json, typeof(IBaseRequest), options);
        deserializeCommand.Should().BeNull();
    }

    private static JsonSerializerOptions SerializerOptions()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true };
        options.Converters.Add(new PolymorphicConverter<IBaseRequest>());

        return options;
    }
}
