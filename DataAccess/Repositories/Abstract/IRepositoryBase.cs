using System.Linq.Expressions;

namespace DataAccess.Repositories.Abstract
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public Task<TEntity?> GetByIdAsync(int id);
        public IQueryable<TEntity> GetQueryable();
        public IQueryable<TEntity> GetFilteredQueryable(Expression<Func<TEntity, bool>> predict);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<IEnumerable<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> predict);
        public Task<(IEnumerable<TEntity> data, int totalCount)> GetPagedAsync(int page, int size);
        public Task<(IEnumerable<TEntity> data, int totalCount)> GetPagedByFilterAsync(Expression<Func<TEntity, bool>> predict, int page, int size);
        public void Delete(TEntity entity);
        public Task CreateAsync(TEntity entity);
        public void Update(TEntity entity);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predict);
        public bool Any(Expression<Func<TEntity, bool>> predict);

    }
}