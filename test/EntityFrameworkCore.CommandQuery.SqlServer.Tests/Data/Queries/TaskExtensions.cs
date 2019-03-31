using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Queries
{
    public static partial class TaskExtensions
    {
        #region Generated Extensions
        public static IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> ByAssignedId(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> queryable, Guid? assignedId)
        {
            return queryable.Where(q => (q.AssignedId == assignedId || (assignedId == null && q.AssignedId == null)));
        }

        public static EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task GetByKey(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> queryable, Guid id)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> GetByKeyAsync(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> queryable, Guid id)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        public static IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> ByPriorityId(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> queryable, Guid? priorityId)
        {
            return queryable.Where(q => (q.PriorityId == priorityId || (priorityId == null && q.PriorityId == null)));
        }

        public static IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> ByStatusId(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> queryable, Guid statusId)
        {
            return queryable.Where(q => q.StatusId == statusId);
        }

        public static IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> ByTenantId(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Task> queryable, Guid tenantId)
        {
            return queryable.Where(q => q.TenantId == tenantId);
        }

        #endregion

    }
}
