using System.Diagnostics;
using System.Text;

namespace MediatR.CommandQuery.Services;

public static class ActivityTimer
{
    private static readonly double _tickFrequency = (double)TimeSpan.TicksPerSecond / Stopwatch.Frequency;

    public static long GetTimestamp() => Stopwatch.GetTimestamp();

    public static TimeSpan GetElapsedTime(long startingTimestamp)
        => GetElapsedTime(startingTimestamp, GetTimestamp());

    public static TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp)
        => new((long)((endingTimestamp - startingTimestamp) * _tickFrequency));
}
