namespace MediatR.CommandQuery.Definitions;

public interface ITrackHistory
{
    DateTime PeriodStart { get; set; }

    DateTime PeriodEnd { get; set; }
}
