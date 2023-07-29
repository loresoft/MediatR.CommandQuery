using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using DataGenerator;

using FluentAssertions;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.EntityFrameworkCore.Handlers;
using MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples;
using MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples.Data;
using MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

using Xunit;

namespace MediatR.CommandQuery.EntityFrameworkCore.Tests.Handlers;

public class EntityCreateCommandHandlerTests
{
    [Fact]
    public async Task CreateLocation()
    {
        var createModel = Generator.Default.Single<LocationCreateModel>();
        createModel.Should().NotBeNull();

        var createCommand = new EntityCreateCommand<LocationCreateModel, LocationReadModel>(MockPrincipal.Default, createModel);
        createCommand.Should().NotBeNull();
        createCommand.Model.Should().NotBeNull();
        createCommand.Principal.Should().NotBeNull();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<LocationCreateModel, Location>();
            cfg.CreateMap<LocationUpdateModel, Location>();
            cfg.CreateMap<Location, LocationReadModel>();
        });
        var mapper = new Mapper(config);

        var options = new DbContextOptionsBuilder<SampleContext>()
            .UseInMemoryDatabase(databaseName: "CreateLocation")
            .Options;

        var context = new SampleContext(options);

        var createHandler = new EntityCreateCommandHandler<SampleContext, Location, Guid, LocationCreateModel, LocationReadModel>(NullLoggerFactory.Instance, context, mapper);
        var readModel = await createHandler.Handle(createCommand, CancellationToken.None);

        readModel.Should().NotBeNull();
        readModel.Id.Should().NotBe(Guid.Empty);
    }
}
