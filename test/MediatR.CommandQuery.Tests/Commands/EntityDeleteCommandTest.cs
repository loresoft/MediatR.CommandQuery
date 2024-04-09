using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Tests.Samples;

namespace MediatR.CommandQuery.Tests.Commands;

public class EntityDeleteCommandTest
{
    [Fact]
    public void ConstructorWithId()
    {
        var id = Guid.NewGuid();
        var deleteCommand = new EntityDeleteCommand<Guid, LocationReadModel>(MockPrincipal.Default, id);

        deleteCommand.Should().NotBeNull();
        deleteCommand.Id.Should().NotBe(Guid.Empty);
        deleteCommand.Id.Should().Be(id);

        deleteCommand.Principal.Should().NotBeNull();

    }
}
