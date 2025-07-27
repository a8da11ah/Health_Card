using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class MedicalReferralService : IServiceBase<MedicalReferral, MedicalReferralFilter>
    {
        private readonly IRepositoryBase<MedicalReferral, MedicalReferralFilter> _medicalReferralRepository;

        public MedicalReferralService(IRepositoryBase<MedicalReferral, MedicalReferralFilter> medicalReferralRepository)
        {
            _medicalReferralRepository = medicalReferralRepository;
        }



        public async Task<IEnumerable<MedicalReferral>> GetAllAsync(MedicalReferralFilter filter)
        {
            return await _medicalReferralRepository.GetAllAsync(filter);
        }

        public async Task<MedicalReferral> GetByIdAsync(int id)
        {
            return await _medicalReferralRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(MedicalReferral medicalReferral)
        {
            await _medicalReferralRepository.CreateAsync(medicalReferral);
        }

        public async Task UpdateAsync(MedicalReferral medicalReferral)
        {
            await _medicalReferralRepository.UpdateAsync(medicalReferral);
        }

        public async Task DeleteAsync(int id)
        {
            await _medicalReferralRepository.DeleteAsync(id);
        }
    }
}
