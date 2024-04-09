using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries;

public static partial class UserRoleExtensions
{
    #region Generated Extensions
    public static System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> ByRoleId(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid roleId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => q.RoleId == roleId);
    }

    public static System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> ByUserId(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid userId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => q.UserId == userId);
    }

    public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole GetByKey(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid userId, Guid roleId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> dbSet)
            return dbSet.Find(userId, roleId);

        return queryable.FirstOrDefault(q => q.UserId == userId
            && q.RoleId == roleId);
    }

    public static async System.Threading.Tasks.ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> GetByKeyAsync(this System.Linq.IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid userId, Guid roleId, System.Threading.CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> dbSet)
            return await dbSet.FindAsync(new object[] { userId, roleId }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.UserId == userId
            && q.RoleId == roleId, cancellationToken);
    }

    #endregion

}
