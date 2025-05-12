using AutoMapper;

using MediatR.CommandQuery.Identity.Commands;
using MediatR.CommandQuery.Identity.Models;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.CommandQuery.Identity.Tests.Handlers;

[Collection(DatabaseCollection.CollectionName)]
public class RegisterWithPasswordTests : DatabaseTestBase
{
    public RegisterWithPasswordTests(ITestOutputHelper output, DatabaseFixture databaseFixture) : base(output, databaseFixture)
    {
    }

    [Fact]
    public async Task RegisterWithPasswordHandler()
    {
        var mediator = ServiceProvider.GetRequiredService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetRequiredService<IMapper>();
        mapper.Should().NotBeNull();

        var generator = new Faker<RegisterWithPasswordModel>()
            .RuleFor(o => o.DisplayName, f => f.Name.FullName())
            .RuleFor(o => o.Email, f => f.Internet.Email())
            .RuleFor(o => o.Password, f => f.Internet.Password(regexPattern: @"[\w!@#$%]"))
            .RuleFor(o => o.PasswordConfirmation, (f, d) => d.Password);

        var model = generator.Generate();

        var command = new RegisterWithPasswordCommand(null, model);
        command.Should().NotBeNull();

        var result = await mediator.Send(command);
        result.Should().NotBeNull();
        result.Succeeded.Should().BeTrue();

    }
}
