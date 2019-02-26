using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataGenerator;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Handlers;
using EntityFrameworkCore.CommandQuery.Tests.Samples;
using EntityFrameworkCore.CommandQuery.Tests.Samples.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.Tests.Handlers
{
    public class EntityUpdateCommandHandlerTests
    {
        [Fact]
        public async Task UpdateLocation()
        {
            var original = Generator.Default.Single<Location>();

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

            var updateModel = Generator.Default.Single<LocationUpdateModel>();
            updateModel.Should().NotBeNull();

            var updateCommand = new EntityUpdateCommand<Guid, LocationUpdateModel, LocationReadModel>(original.Id, updateModel, MockPrincipal.Default);
            updateCommand.Should().NotBeNull();
            updateCommand.Model.Should().NotBeNull();
            updateCommand.Principal.Should().NotBeNull();

            var updateCommandHandler = new EntityUpdateCommandHandler<SampleContext, Location, Guid, LocationUpdateModel, LocationReadModel>(NullLoggerFactory.Instance, context, mapper);
            var readModel = await updateCommandHandler.Handle(updateCommand, CancellationToken.None);

            readModel.Should().NotBeNull();
            readModel.Id.Should().NotBe(Guid.Empty);
            readModel.Id.Should().Be(original.Id);

            readModel.Name.Should().Be(updateModel.Name);
        }

    }
}