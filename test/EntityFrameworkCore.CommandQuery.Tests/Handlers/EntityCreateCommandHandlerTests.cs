using System;
using System.Collections.Generic;
using System.Text;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.Tests.Handlers
{
    public class EntityCreateCommandHandlerTests
    {
        [Fact]
        public async Task CreateLocation()
        {
            var createModel = Generator.Default.Single<LocationCreateModel>();
            createModel.Should().NotBeNull();

            var createCommand = new EntityCreateCommand<LocationCreateModel, LocationReadModel>(createModel, MockPrincipal.Default);
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
}
