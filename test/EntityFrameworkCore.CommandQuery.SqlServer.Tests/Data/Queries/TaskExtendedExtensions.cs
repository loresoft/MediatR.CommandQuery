using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Queries
{
    public static partial class TaskExtendedExtensions
    {
        #region Generated Extensions
        public static EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended GetByKey(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended> dbSet)
                return dbSet.Find(taskId);

            return queryable.FirstOrDefault(q => q.TaskId == taskId);
        }

        public static Task<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended> GetByKeyAsync(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.TaskExtended> dbSet)
                return dbSet.FindAsync(taskId);

            return queryable.FirstOrDefaultAsync(q => q.TaskId == taskId);
        }

        #endregion

    }
}
