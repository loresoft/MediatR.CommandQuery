using System;
using System.Linq;
using System.Threading.Tasks;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries;

public static partial class TaskExtendedExtensions
{
    #region Generated Extensions
    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended GetByKey(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> queryable, Guid taskId)
    {
        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> dbSet)
            return dbSet.Find(taskId);

        return queryable.FirstOrDefault(q => q.TaskId == taskId);
    }

    public static ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> GetByKeyAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> queryable, Guid taskId)
    {
        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended> dbSet)
            return dbSet.FindAsync(taskId);

        var task = queryable.FirstOrDefaultAsync(q => q.TaskId == taskId);
        return new ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended>(task);
    }

    #endregion

}
