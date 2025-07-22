using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Model;

namespace Health_Card.Interface.WorkInjury
{
    public interface IWorkInjuryService
    {
        Task<IEnumerable<Model.WorkInjury>> GetAllAsync();
        Task<Model.WorkInjury> GetByIdAsync(int id);
        Task CreateAsync(Model.WorkInjury workInjury);
        Task UpdateAsync(Model.WorkInjury workInjury);
        Task DeleteAsync(int id);
    }
}
