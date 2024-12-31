namespace MediatR.CommandQuery.Definitions;


/// <summary>
/// An <c>interface</c> indicating the implemented type supports tracking temporal history (SQL Server Temporal tables)
/// </summary>
public interface ITrackHistory
{
    /// <summary>
    /// Gets or sets the temporal period start.
    /// </summary>
    /// <value>
    /// The temporal period start.
    /// </value>
    DateTime PeriodStart { get; set; }

    /// <summary>
    /// Gets or sets the temporal period end.
    /// </summary>
    /// <value>
    /// The temporal period end.
    /// </value>
    DateTime PeriodEnd { get; set; }
}
