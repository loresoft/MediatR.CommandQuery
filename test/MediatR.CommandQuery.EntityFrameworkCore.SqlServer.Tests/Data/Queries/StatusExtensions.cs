using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries;

public static partial class StatusExtensions
{
    #region Generated Extensions
    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Status GetByKey(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Status> queryable, Guid id)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Status> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    public static async System.Threading.Tasks.ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Status> GetByKeyAsync(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Status> queryable, Guid id, System.Threading.CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Status> dbSet)
            return await dbSet.FindAsync(new object[] { id }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    #endregion

}
