using Microsoft.EntityFrameworkCore;
using ServiceAgency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceAgency.Domain.Repository
{
    public interface IBaseRepository<TE>
        where TE : Entity
    {
        Task<TE> AddAsync(TE entity);
        Task UpdateAsync(TE entity);
        Task UpdateAsync(int id, Expression<Func<TE, TE>> updateExpression);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(TE entity);
        Task DeleteAsync(Expression<Func<TE, bool>> expression);
        Task<TE> GetByIdAsync(int id, params Expression<Func<TE, object>>[] eagerSelectors);
        Task<TE> FirstOrDefaultAsync(Expression<Func<TE, bool>> expression, params Expression<Func<TE, object>>[] eagerSelectors);
        Task<List<TE>> ListAsync(Expression<Func<TE, bool>> expression, params Expression<Func<TE, object>>[] eagerSelectors);
        Task<IReadOnlyList<TE>> QueryListAsync(Expression<Func<TE, bool>> filter = null, int page = 1, int pageSize = 10);
        Task<bool> AnyAsync(Expression<Func<TE, bool>> expression = null);
        Task<int> CountAsync(Expression<Func<TE, bool>> expression = null);
        IQueryable<TE> Queryable();
    }
}
