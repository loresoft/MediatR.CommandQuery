using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Queries
{
    public static partial class RoleExtensions
    {
        #region Generated Extensions
        public static EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role GetByKey(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role> queryable, Guid id)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role> GetByKeyAsync(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role> queryable, Guid id)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        public static EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role GetByName(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role> queryable, string name)
        {
            return queryable.FirstOrDefault(q => q.Name == name);
        }

        public static Task<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role> GetByNameAsync(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Role> queryable, string name)
        {
            return queryable.FirstOrDefaultAsync(q => q.Name == name);
        }

        #endregion

    }
}
