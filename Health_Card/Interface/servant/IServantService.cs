using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Model;

namespace Health_Card.Interface
{
    public interface IServantService
    {
        Task<IEnumerable<Servant>> GetAllAsync();
        Task<Servant> GetByIdAsync(int id);
        Task CreateAsync(Servant servant);
        Task UpdateAsync(Servant servant);
        Task DeleteAsync(int id);
    }
}