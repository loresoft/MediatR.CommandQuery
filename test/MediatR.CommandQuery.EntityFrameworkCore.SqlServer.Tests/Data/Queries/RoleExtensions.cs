using System;
using System.Linq;
using System.Threading.Tasks;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries;

public static partial class RoleExtensions
{
    #region Generated Extensions
    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role GetByKey(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role> queryable, Guid id)
    {
        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    public static ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role> GetByKeyAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role> queryable, Guid id)
    {
        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role> dbSet)
            return dbSet.FindAsync(id);

        var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
        return new ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role>(task);
    }

    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role GetByName(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role> queryable, string name)
    {
        return queryable.FirstOrDefault(q => q.Name == name);
    }

    public static Task<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role> GetByNameAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Role> queryable, string name)
    {
        return queryable.FirstOrDefaultAsync(q => q.Name == name);
    }

    #endregion

}
