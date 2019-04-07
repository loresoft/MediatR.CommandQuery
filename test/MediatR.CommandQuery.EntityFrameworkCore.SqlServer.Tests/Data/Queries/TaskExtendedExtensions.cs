using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class TaskExtendedExtensions
    {
        #region Generated Extensions
        public static TaskExtended GetByKey(this IQueryable<TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<TaskExtended> dbSet)
                return dbSet.Find(taskId);

            return queryable.FirstOrDefault(q => q.TaskId == taskId);
        }

        public static Task<TaskExtended> GetByKeyAsync(this IQueryable<TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<TaskExtended> dbSet)
                return dbSet.FindAsync(taskId);

            return queryable.FirstOrDefaultAsync(q => q.TaskId == taskId);
        }

        #endregion

    }
}
