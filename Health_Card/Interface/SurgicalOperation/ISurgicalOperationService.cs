using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Model;

namespace Health_Card.Interface.SurgicalOperation
{
    public interface ISurgicalOperationService
    {
        Task<IEnumerable<Model.SurgicalOperation>> GetAllAsync();
        Task<Model.SurgicalOperation> GetByIdAsync(int id);
        Task CreateAsync(Model.SurgicalOperation surgicalOperation);
        Task UpdateAsync(Model.SurgicalOperation surgicalOperation);
        Task DeleteAsync(int id);
    }
}
