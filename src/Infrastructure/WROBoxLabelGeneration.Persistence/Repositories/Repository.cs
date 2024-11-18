using WROBoxLabelGeneration.Application.Contracts.Persistence;
using WROBoxLabelGeneration.Persistence.DbContexts;

namespace WROBoxLabelGeneration.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ShipBobLiveDbContext _dbContext;

        public Repository(ShipBobLiveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
