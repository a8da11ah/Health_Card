using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface.ServantMedicalReview;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class ServantMedicalReviewService : IServantMedicalReviewService
    {
        private readonly IServantMedicalReviewRepository _servantMedicalReviewRepository;

        public ServantMedicalReviewService(IServantMedicalReviewRepository servantMedicalReviewRepository)
        {
            _servantMedicalReviewRepository = servantMedicalReviewRepository;
        }

        public async Task<IEnumerable<ServantMedicalReview>> GetAllAsync()
        {
            return await _servantMedicalReviewRepository.GetAllAsync();
        }

        public async Task<ServantMedicalReview> GetByIdAsync(int id)
        {
            return await _servantMedicalReviewRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(ServantMedicalReview servantMedicalReview)
        {
            await _servantMedicalReviewRepository.CreateAsync(servantMedicalReview);
        }

        public async Task UpdateAsync(ServantMedicalReview servantMedicalReview)
        {
            await _servantMedicalReviewRepository.UpdateAsync(servantMedicalReview);
        }

        public async Task DeleteAsync(int id)
        {
            await _servantMedicalReviewRepository.DeleteAsync(id);
        }
    }
}
