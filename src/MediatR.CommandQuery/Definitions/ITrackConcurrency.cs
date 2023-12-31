namespace MediatR.CommandQuery.Definitions;

public interface ITrackConcurrency
{
    long RowVersion { get; set; }
}
