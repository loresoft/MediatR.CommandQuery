using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;

using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Tests.Samples;

namespace MediatR.CommandQuery.Tests.Extensions;

public class QueryExtensionsTests
{
    [Fact]
    public void PageNormal()
    {
        var generator = new Faker<Fruit>()
            .RuleFor(p => p.Id, (faker, model) => Guid.NewGuid())
            .RuleFor(p => p.Name, (faker, model) => faker.Name.FirstName())
            .RuleFor(p => p.Rank, (faker, model) => faker.Random.Int(1, 10));

        var fruits = generator.Generate(20);
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Page(1, 10)
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(10);
    }

    [Fact]
    public void PageNextPage()
    {
        var generator = new Faker<Fruit>()
            .RuleFor(p => p.Id, (faker, model) => Guid.NewGuid())
            .RuleFor(p => p.Name, (faker, model) => faker.Name.FirstName())
            .RuleFor(p => p.Rank, (faker, model) => faker.Random.Int(1, 10));

        var fruits = generator.Generate(20);
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Page(2, 10)
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(10);

        list[0].Id.Should().Be(fruits[10].Id);
    }

    [Fact]
    public void PageNextPageOutOfRange()
    {
        var generator = new Faker<Fruit>()
            .RuleFor(p => p.Id, (faker, model) => Guid.NewGuid())
            .RuleFor(p => p.Name, (faker, model) => faker.Name.FirstName())
            .RuleFor(p => p.Rank, (faker, model) => faker.Random.Int(1, 10));

        var fruits = generator.Generate(20);
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Page(3, 10)
            .ToList();

        list.Should().BeEmpty();
    }

    [Fact]
    public void PageIndexOutOfRange()
    {
        var generator = new Faker<Fruit>()
            .RuleFor(p => p.Id, (faker, model) => Guid.NewGuid())
            .RuleFor(p => p.Name, (faker, model) => faker.Name.FirstName())
            .RuleFor(p => p.Rank, (faker, model) => faker.Random.Int(1, 10));

        var fruits = generator.Generate(20);
        fruits.Should().NotBeEmpty();

        Action act = () => fruits
            .AsQueryable()
            .Page(0, 0)
            .ToList();

        act.Should().Throw<ArgumentOutOfRangeException>();
    }


    [Fact]
    public void SortNormal()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Sort(new[] { new EntitySort { Name = "Name" } })
            .ToList();

        list.Should().NotBeEmpty();
        list[0].Name.Should().Be("Apple");
        list[9].Name.Should().Be("Strawberry");
    }

    [Fact]
    public void SortMixedCase()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Sort(new[] { new EntitySort { Name = "name" } })
            .ToList();

        list.Should().NotBeEmpty();
        list[0].Name.Should().Be("Apple");
        list[9].Name.Should().Be("Strawberry");
    }

    [Fact]
    public void SortDescending()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Sort(new[] { new EntitySort { Name = "Name", Direction = "Descending" } })
            .ToList();

        list.Should().NotBeEmpty();
        list[0].Name.Should().Be("Strawberry");
        list[9].Name.Should().Be("Apple");
    }

    [Fact]
    public void SortMultiple()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Sort(new[]
            {
                new EntitySort { Name = "Rank" },
                new EntitySort { Name = "Name" }
            })
            .ToList();

        list.Should().NotBeEmpty();

        list[0].Name.Should().Be("Pear");

        list[6].Name.Should().Be("Blueberry");
        list[7].Name.Should().Be("Raspberry");
        list[8].Name.Should().Be("Strawberry");

        list[9].Name.Should().Be("Banana");
    }

    [Fact]
    public void SortNull()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Sort((EntitySort)null)
            .ToList();

        list.Should().NotBeEmpty();
        list[0].Name.Should().Be("Pear");
        list[9].Name.Should().Be("Raspberry");
    }

    [Fact]
    public void SortEmpty()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Sort(Enumerable.Empty<EntitySort>())
            .ToList();

        list.Should().NotBeEmpty();
        list[0].Name.Should().Be("Pear");
        list[9].Name.Should().Be("Raspberry");
    }

    [Fact]
    public void SortInvalidName()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        Action act = () => fruits
            .AsQueryable()
            .Sort(new[] { new EntitySort { Name = "Blah" } })
            .ToList();

        act.Should().Throw<ParseException>();
    }



    [Fact]
    public void FilterNormal()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter { Name = "Rank", Value = 7 })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(3);
    }

    [Fact]
    public void FilterLogicalOr()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter
            {
                Logic = "or",
                Filters = new List<EntityFilter>
                {
                    new EntityFilter{ Name = "Rank", Value = 7 },
                    new EntityFilter{ Name = "Name", Value = "Apple" }
                }
            })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(4);
    }

    [Fact]
    public void FilterLogicalAnd()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter
            {
                Filters = new List<EntityFilter>
                {
                    new EntityFilter{ Name = "Rank", Value = 7 },
                    new EntityFilter{ Name = "Name", Value = "Blueberry" }
                }

            })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(1);
    }

    [Fact]
    public void FilterComplex()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter
            {
                Filters = new List<EntityFilter>
                {
                    new EntityFilter{ Name = "Rank", Operator = ">", Value = 5 },
                    new EntityFilter
                    {
                        Logic = "or",
                        Filters = new List<EntityFilter>
                        {
                            new EntityFilter{ Name = "Name", Value = "Strawberry" },
                            new EntityFilter{ Name = "Name", Value = "Blueberry" }
                        }
                    }
                }
            })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(2);
    }

    [Fact]
    public void FilterContains()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter { Name = "Name", Operator = "Contains", Value = "berry" })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(3);
    }

    [Fact]
    public void FilterNotContains()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter { Name = "Name", Operator = "!Contains", Value = "berry" })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(7);
    }

    [Fact]
    public void FilterStartsWith()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter { Name = "Name", Operator = "StartsWith", Value = "P" })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(3);
    }

    [Fact]
    public void FilterNotStartsWith()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter { Name = "Name", Operator = "!StartsWith", Value = "P" })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(7);
    }

    [Fact]
    public void FilterEndsWith()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter { Name = "Name", Operator = "EndsWith", Value = "berry" })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(3);
    }

    [Fact]
    public void FilterNotEndsWith()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter { Name = "Name", Operator = "!EndsWith", Value = "berry" })
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(7);
    }

    [Fact]
    public void FilterNull()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(null)
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(10);
    }

    [Fact]
    public void FilterEmpty()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        var list = fruits
            .AsQueryable()
            .Filter(new EntityFilter())
            .ToList();

        list.Should().NotBeEmpty();
        list.Count.Should().Be(10);
    }

    [Fact]
    public void FilterInvalidName()
    {
        var fruits = Fruit.Data();
        fruits.Should().NotBeEmpty();

        Action act = () => fruits
            .AsQueryable()
            .Filter(new EntityFilter { Name = "Blah", Value = 7 })
            .ToList();

        act.Should().Throw<ParseException>();
    }

}
