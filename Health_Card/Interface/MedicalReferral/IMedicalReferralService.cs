using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Model;

namespace Health_Card.Interface.MedicalReferral
{
    public interface IMedicalReferralService
    {
        Task<IEnumerable<Model.MedicalReferral>> GetAllAsync();
        Task<Model.MedicalReferral> GetByIdAsync(int id);
        Task CreateAsync(Model.MedicalReferral medicalReferral);
        Task UpdateAsync(Model.MedicalReferral medicalReferral);
        Task DeleteAsync(int id);
    }
}
