using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Model;

namespace Health_Card.Interface.Vaccination
{
    public interface IVaccinationService
    {
        Task<IEnumerable<Model.Vaccination>> GetAllAsync();
        Task<Model.Vaccination> GetByIdAsync(int id);
        Task CreateAsync(Model.Vaccination vaccination);
        Task UpdateAsync(Model.Vaccination vaccination);
        Task DeleteAsync(int id);
    }
}
