using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class ServantMedicalReviewService : IServiceBase<ServantMedicalReview,ServantMedicalReviewFilter>
    {
        private readonly IRepositoryBase<ServantMedicalReview,ServantMedicalReviewFilter> _servantMedicalReviewRepository;

        public ServantMedicalReviewService(IRepositoryBase<ServantMedicalReview,ServantMedicalReviewFilter> servantMedicalReviewRepository)
        {
            _servantMedicalReviewRepository = servantMedicalReviewRepository;
        }

        public async Task<IEnumerable<ServantMedicalReview>> GetAllAsync(ServantMedicalReviewFilter  filter)
        {
            return await _servantMedicalReviewRepository.GetAllAsync(filter);
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
