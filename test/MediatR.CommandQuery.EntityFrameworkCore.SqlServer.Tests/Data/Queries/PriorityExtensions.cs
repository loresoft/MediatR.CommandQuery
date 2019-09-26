using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class PriorityExtensions
    {
        #region Generated Extensions
        public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Priority GetByKey(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Priority> queryable, Guid id)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Priority> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Priority> GetByKeyAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Priority> queryable, Guid id)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Priority> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Priority>(task);
        }

        #endregion

    }
}
