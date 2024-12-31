namespace MediatR.CommandQuery.Definitions;

/// <summary>
///   <c>Interface</c> indicating implemented type supports multi-tenancy
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
public interface IHaveTenant<TKey>
{
    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    /// <value>
    /// The tenant identifier.
    /// </value>
    TKey TenantId { get; set; }
}
