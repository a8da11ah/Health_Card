using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Model;

namespace Health_Card.Interface.ServantChronicTreatment
{
    public interface IServantChronicTreatmentService
    {
        Task<IEnumerable<Model.ServantChronicTreatment>> GetAllAsync();
        Task<Model.ServantChronicTreatment> GetByIdAsync(int id);
        Task CreateAsync(Model.ServantChronicTreatment servantChronicTreatment);
        Task UpdateAsync(Model.ServantChronicTreatment servantChronicTreatment);
        Task DeleteAsync(int id);
    }
}
