using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Task = MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries;

public static partial class TaskExtensions
{
    #region Generated Extensions
    public static IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> ByAssignedId(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> queryable, Guid? assignedId)
    {
        return queryable.Where(q => (q.AssignedId == assignedId || (assignedId == null && q.AssignedId == null)));
    }

    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task GetByKey(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> queryable, Guid id)
    {
        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    public static ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> GetByKeyAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> queryable, Guid id)
    {
        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> dbSet)
            return dbSet.FindAsync(id);

        var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
        return new ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task>(task);
    }

    public static IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> ByPriorityId(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> queryable, Guid? priorityId)
    {
        return queryable.Where(q => (q.PriorityId == priorityId || (priorityId == null && q.PriorityId == null)));
    }

    public static IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> ByStatusId(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> queryable, Guid statusId)
    {
        return queryable.Where(q => q.StatusId == statusId);
    }

    public static IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> ByTenantId(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Task> queryable, Guid tenantId)
    {
        return queryable.Where(q => q.TenantId == tenantId);
    }

    #endregion

}
