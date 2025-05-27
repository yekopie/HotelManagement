using System.Linq.Expressions;

namespace DataAccess.Repositories.Abstract
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public Task<TEntity?> GetByIdAsync(int id);
        public IQueryable<TEntity> GetAll();
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public Task CreateAsync(TEntity entity);
        public IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> predict);
    }
}