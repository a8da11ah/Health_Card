using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Model;

namespace Health_Card.Interface.ServantMedicalReview
{
    public interface IServantMedicalReviewService
    {
        Task<IEnumerable<Model.ServantMedicalReview>> GetAllAsync();
        Task<Model.ServantMedicalReview> GetByIdAsync(int id);
        Task CreateAsync(Model.ServantMedicalReview servantMedicalReview);
        Task UpdateAsync(Model.ServantMedicalReview servantMedicalReview);
        Task DeleteAsync(int id);
    }
}
