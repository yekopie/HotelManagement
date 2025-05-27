namespace DataAccess.Repositories.Abstract
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public Task<TEntity> GetByIdAsync(int id);
        public IQueryable<TEntity> GetAll();
        public Task UpdateAsync(int id, TEntity entity);
        public Task Delete(TEntity entity);
        public Task CreateAsync(TEntity entity);
    }
}