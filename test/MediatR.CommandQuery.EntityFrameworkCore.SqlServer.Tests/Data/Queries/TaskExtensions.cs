using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task = MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class TaskExtensions
    {
        #region Generated Extensions
        public static IQueryable<Task> ByAssignedId(this IQueryable<Task> queryable, Guid? assignedId)
        {
            return queryable.Where(q => (q.AssignedId == assignedId || (assignedId == null && q.AssignedId == null)));
        }

        public static Task GetByKey(this IQueryable<Task> queryable, Guid id)
        {
            if (queryable is DbSet<Task> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<Task> GetByKeyAsync(this IQueryable<Task> queryable, Guid id)
        {
            if (queryable is DbSet<Task> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        public static IQueryable<Task> ByPriorityId(this IQueryable<Task> queryable, Guid? priorityId)
        {
            return queryable.Where(q => (q.PriorityId == priorityId || (priorityId == null && q.PriorityId == null)));
        }

        public static IQueryable<Task> ByStatusId(this IQueryable<Task> queryable, Guid statusId)
        {
            return queryable.Where(q => q.StatusId == statusId);
        }

        public static IQueryable<Task> ByTenantId(this IQueryable<Task> queryable, Guid tenantId)
        {
            return queryable.Where(q => q.TenantId == tenantId);
        }

        #endregion

    }
}
