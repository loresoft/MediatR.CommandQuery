using System.Diagnostics;

namespace MediatR.CommandQuery.Services;

public static class ActivityTimer
{
    public static long GetTimestamp() => Stopwatch.GetTimestamp();

    public static TimeSpan GetElapsedTime(long startingTimestamp)
        => GetElapsedTime(startingTimestamp, GetTimestamp());

    public static TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp)
        => Stopwatch.GetElapsedTime(startingTimestamp, endingTimestamp);
}
