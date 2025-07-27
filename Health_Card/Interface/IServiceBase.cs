

namespace Health_Card.Interface
{
    public interface IServiceBase<T,TFilter>
    {
        Task<IEnumerable<T>> GetAllAsync(TFilter filter);
        // Task<IEnumerable<T>> GetByFilterAsync(T filter);

        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}