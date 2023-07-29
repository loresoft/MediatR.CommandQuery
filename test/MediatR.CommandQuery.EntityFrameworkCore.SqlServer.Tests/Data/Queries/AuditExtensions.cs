using System;
using System.Linq;
using System.Threading.Tasks;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries;

public static partial class AuditExtensions
{
    #region Generated Extensions
    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Audit GetByKey(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Audit> queryable, Guid id)
    {
        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Audit> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    public static ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Audit> GetByKeyAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Audit> queryable, Guid id)
    {
        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Audit> dbSet)
            return dbSet.FindAsync(id);

        var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
        return new ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Audit>(task);
    }

    #endregion

}
