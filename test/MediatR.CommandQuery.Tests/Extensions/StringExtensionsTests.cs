using MediatR.CommandQuery.Extensions;

namespace MediatR.CommandQuery.Tests.Extensions;

public class StringExtensionsTests
{
    [Fact]
    public void CombineTests()
    {
        var result = "/".Combine("/api/user");
        Assert.Equal("/api/user", result);

        result = "/api".Combine("/user");
        Assert.Equal("/api/user", result);

        result = "/api/".Combine("user");
        Assert.Equal("/api/user", result);

        result = "/api".Combine("user");
        Assert.Equal("/api/user", result);
    }
}
