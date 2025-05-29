using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Concrete
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private AppDbContext _context;
        private DbSet<TEntity> _dbSet;
        public RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> predict)
        {
            return await _dbSet.AsNoTracking().Where(predict).ToListAsync();
        }

        public IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> predict)
        {
            return _dbSet.Where(predict).AsNoTracking();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetFilteredQueryable(Expression<Func<TEntity, bool>> predict)
        {
            return _dbSet.AsNoTracking().Where(predict);
        }

        public async Task<(IEnumerable<TEntity> data, int totalCount)> GetPagedAsync(int page, int size)
        {
            int totalCount = await _dbSet.AsNoTracking().CountAsync();
            IEnumerable<TEntity> data = await _dbSet.AsNoTracking().Skip((page - 1) * size).Take(size).ToListAsync();

            return (data, totalCount);
        }

        public async Task<(IEnumerable<TEntity> data, int totalCount)> GetPagedByFilterAsync(Expression<Func<TEntity, bool>> predict, int page, int size)
        {
            int totalCount = await _dbSet.AsNoTracking().Where(predict).CountAsync();
            IEnumerable<TEntity> data = await _dbSet.AsNoTracking().Where(predict)
                .Skip((page - 1) * size).Take(size).ToListAsync();

            return (data, totalCount);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsNoTracking();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}