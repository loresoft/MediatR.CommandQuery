using AutoMapper;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.EntityFrameworkCore.Handlers;
using MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples;
using MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples.Data;
using MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace MediatR.CommandQuery.EntityFrameworkCore.Tests.Handlers;

public class EntityUpdateCommandHandlerTests
{
    [Fact]
    public async Task UpdateLocation()
    {
        var generatorLocation = new Faker<Location>()
            .RuleFor(p => p.Name, (faker, model) => faker.Company.CompanyName())
            .RuleFor(p => p.Description, (faker, model) => faker.Lorem.Sentence())
            .RuleFor(p => p.AddressLine1, (faker, model) => faker.Address.StreetAddress())
            .RuleFor(p => p.AddressLine2, (faker, model) => faker.Address.SecondaryAddress())
            .RuleFor(p => p.City, (faker, model) => faker.Address.City())
            .RuleFor(p => p.StateProvince, (faker, model) => faker.Address.StateAbbr())
            .RuleFor(p => p.PostalCode, (faker, model) => faker.Address.ZipCode())
            .RuleFor(p => p.Latitude, (faker, model) => (decimal)faker.Address.Latitude())
            .RuleFor(p => p.Longitude, (faker, model) => (decimal)faker.Address.Longitude());

        var original = generatorLocation.Generate();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<LocationCreateModel, Location>();
            cfg.CreateMap<LocationUpdateModel, Location>();
            cfg.CreateMap<Location, LocationReadModel>();
        });
        var mapper = new Mapper(config);

        var options = new DbContextOptionsBuilder<SampleContext>()
            .UseInMemoryDatabase(databaseName: "UpdateLocation")
            .Options;

        var context = new SampleContext(options);
        context.Locations.Add(original);
        context.SaveChanges();

        var generatorUpdateModel = new Faker<LocationUpdateModel>()
            .RuleFor(p => p.Name, (faker, model) => faker.Company.CompanyName())
            .RuleFor(p => p.Description, (faker, model) => faker.Lorem.Sentence())
            .RuleFor(p => p.AddressLine1, (faker, model) => faker.Address.StreetAddress())
            .RuleFor(p => p.AddressLine2, (faker, model) => faker.Address.SecondaryAddress())
            .RuleFor(p => p.City, (faker, model) => faker.Address.City())
            .RuleFor(p => p.StateProvince, (faker, model) => faker.Address.StateAbbr())
            .RuleFor(p => p.PostalCode, (faker, model) => faker.Address.ZipCode())
            .RuleFor(p => p.Latitude, (faker, model) => (decimal)faker.Address.Latitude())
            .RuleFor(p => p.Longitude, (faker, model) => (decimal)faker.Address.Longitude());

        var updateModel = generatorUpdateModel.Generate();
        updateModel.Should().NotBeNull();

        var updateCommand = new EntityUpdateCommand<Guid, LocationUpdateModel, LocationReadModel>(MockPrincipal.Default, original.Id, updateModel);
        updateCommand.Should().NotBeNull();
        updateCommand.Model.Should().NotBeNull();
        updateCommand.Principal.Should().NotBeNull();

        var updateCommandHandler = new EntityUpdateCommandHandler<SampleContext, Location, Guid, LocationUpdateModel, LocationReadModel>(NullLoggerFactory.Instance, context, mapper);
        var readModel = await updateCommandHandler.Handle(updateCommand, CancellationToken.None);

        readModel.Should().NotBeNull();
        readModel.Value.Id.Should().NotBe(Guid.Empty);
        readModel.Value.Id.Should().Be(original.Id);

        readModel.Value.Name.Should().Be(updateModel.Name);
    }

}
