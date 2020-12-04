using Microsoft.EntityFrameworkCore;
using ServiceAgency.Domain.Entities;
using ServiceAgency.Domain.Exceptions;
using ServiceAgency.Domain.Repository;
using ServiceAgency.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceAgency.Infrastructure.Repo
{
    public class MssqlRepository<TE> : IBaseRepository<TE>
    where TE : Entity
    {
        private readonly AppDbContext _dbContext;

        public MssqlRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TE> AddAsync(TE entity)
        {
            await DbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TE entity)
        {
            DbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Expression<Func<TE, TE>> updateExpression)
        {
            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity == default)
                throw new NotFoundException(id, typeof(TE));

            var func = updateExpression.Compile();
            var updatedEntity = func(entity);

            if (!updatedEntity.Id.Equals(entity.Id))
                throw new ConcurrencyException("Id of entity isn't updatable");

            DbSet.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (entity != default)
            {
                DbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(TE entity)
        {
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expression<Func<TE, bool>> expression)
        {
            var entities = await DbSet.Where(expression).ToListAsync();

            if (entities != null && entities.Any())
            {
                DbSet.RemoveRange(entities);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<TE> GetByIdAsync(int id, params Expression<Func<TE, object>>[] eagerSelectors)
        {
            IQueryable<TE> queryable = _dbContext.Set<TE>();
            if (eagerSelectors.Any())
                queryable = eagerSelectors.Aggregate(queryable, (current, t) => current.Include(t));

            return await queryable.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<TE> FirstOrDefaultAsync(Expression<Func<TE, bool>> expression, params Expression<Func<TE, object>>[] eagerSelectors)
        {
            IQueryable<TE> queryable = _dbContext.Set<TE>();
            if (eagerSelectors.Any())
                queryable = eagerSelectors.Aggregate(queryable, (current, t) => current.Include(t));

            return await queryable.FirstOrDefaultAsync(expression);
        }

        public async Task<IReadOnlyList<TE>> QueryListAsync(Expression<Func<TE, bool>> filter = null, int page = 1, int pageSize = 10)
        {
            if (filter != null)
            {
                return await _dbContext.Set<TE>()
                    ?.Where(filter)
                    ?.Skip((page - 1) * pageSize)
                    ?.Take(pageSize)
                    ?.AsNoTracking()
                    .ToListAsync();
            }

            return await _dbContext.Set<TE>()
                ?.Skip((page - 1) * pageSize)
                ?.Take(pageSize)
                ?.AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<TE>> ListAsync(Expression<Func<TE, bool>> expression, params Expression<Func<TE, object>>[] eagerSelectors)
        {
            IQueryable<TE> queryable = _dbContext.Set<TE>();
            if (eagerSelectors.Any())
                queryable = eagerSelectors.Aggregate(queryable, (current, t) => current.Include(t));

            return await queryable.Where(expression).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TE, bool>> expression = null)
        {
            return expression == null
                ? await DbSet.AnyAsync()
                : await DbSet.AnyAsync(expression);
        }

        public async Task<int> CountAsync(Expression<Func<TE, bool>> expression = null)
        {
            return expression == null
                ? await DbSet.CountAsync()
                : await DbSet.CountAsync(expression);
        }

        public IQueryable<TE> Queryable()
        {
            return DbSet;
        }

        private DbSet<TE> DbSet => _dbContext.Set<TE>();
    }
}
