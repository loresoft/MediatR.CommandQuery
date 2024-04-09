using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Tests.Samples;

namespace MediatR.CommandQuery.Tests.Commands;

public class EntityUpdateCommandTests
{
    [Fact]
    public void ConstructorNullModel()
    {
        Action act = () => new EntityUpdateCommand<Guid, LocationUpdateModel, LocationReadModel>(null, Guid.Empty, null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ConstructorWithModel()
    {
        var id = Guid.NewGuid();
        var generator = new Faker<LocationUpdateModel>()
            .RuleFor(p => p.Name, (faker, model) => faker.Company.CompanyName())
            .RuleFor(p => p.Description, (faker, model) => faker.Lorem.Sentence())
            .RuleFor(p => p.AddressLine1, (faker, model) => faker.Address.StreetAddress())
            .RuleFor(p => p.AddressLine2, (faker, model) => faker.Address.SecondaryAddress())
            .RuleFor(p => p.City, (faker, model) => faker.Address.City())
            .RuleFor(p => p.StateProvince, (faker, model) => faker.Address.StateAbbr())
            .RuleFor(p => p.PostalCode, (faker, model) => faker.Address.ZipCode())
            .RuleFor(p => p.Latitude, (faker, model) => (decimal)faker.Address.Latitude())
            .RuleFor(p => p.Longitude, (faker, model) => (decimal)faker.Address.Longitude());

        var updateModel = generator.Generate();
        updateModel.Should().NotBeNull();

        var updateCommand = new EntityUpdateCommand<Guid, LocationUpdateModel, LocationReadModel>(MockPrincipal.Default, id, updateModel);
        updateCommand.Should().NotBeNull();

        updateCommand.Id.Should().NotBe(Guid.Empty);
        updateCommand.Id.Should().Be(id);

        updateCommand.Model.Should().NotBeNull();

        updateCommand.Principal.Should().NotBeNull();
    }
}
