

namespace Health_Card.Interface
{
    public interface IRepositoryBase<T,TFilter>
    {
        Task<IEnumerable<T>> GetAllAsync(TFilter filter);
        // Task<IEnumerable<T>> GetByFilterAsync(BaseFilter filter);

        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}