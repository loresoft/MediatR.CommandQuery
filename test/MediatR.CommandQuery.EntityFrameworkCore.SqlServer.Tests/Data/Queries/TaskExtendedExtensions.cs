using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries;

public static partial class TaskExtendedExtensions
{
    #region Generated Extensions
    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended GetByKey(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> queryable, Guid taskId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> dbSet)
            return dbSet.Find(taskId);

        return queryable.FirstOrDefault(q => q.TaskId == taskId);
    }

    public static async System.Threading.Tasks.ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> GetByKeyAsync(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> queryable, Guid taskId, System.Threading.CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> dbSet)
            return await dbSet.FindAsync(new object[] { taskId }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.TaskId == taskId, cancellationToken);
    }

    #endregion

}
