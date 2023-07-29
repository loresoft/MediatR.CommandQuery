using System;

using Xunit.Abstractions;

namespace MediatR.CommandQuery.EntityFrameworkCore.Tests;

public abstract class UnitTestBase
{
    protected UnitTestBase(ITestOutputHelper outputHelper)
    {
        OutputHelper = outputHelper;

    }

    public ITestOutputHelper OutputHelper { get; }
}
