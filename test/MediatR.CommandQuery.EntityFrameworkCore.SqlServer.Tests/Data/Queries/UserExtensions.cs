using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries;

public static partial class UserExtensions
{
    #region Generated Extensions
    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User GetByEmailAddress(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> queryable, string emailAddress)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.FirstOrDefault(q => q.EmailAddress == emailAddress);
    }

    public static async System.Threading.Tasks.Task<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> GetByEmailAddressAsync(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> queryable, string emailAddress, System.Threading.CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return await queryable.FirstOrDefaultAsync(q => q.EmailAddress == emailAddress, cancellationToken);
    }

    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User GetByKey(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> queryable, Guid id)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    public static async System.Threading.Tasks.ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> GetByKeyAsync(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> queryable, Guid id, System.Threading.CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> dbSet)
            return await dbSet.FindAsync(new object[] { id }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    #endregion

}
