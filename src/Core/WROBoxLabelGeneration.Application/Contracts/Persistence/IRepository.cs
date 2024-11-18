namespace WROBoxLabelGeneration.Application.Contracts.Persistence
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public TEntity? GetById(int id);

        Task<TEntity?> GetByIdAsync(int id);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task SaveChangesAsync();
    }
}
