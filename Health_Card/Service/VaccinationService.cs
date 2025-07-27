
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class VaccinationService : IServiceBase<Vaccination,VaccinationFilter>
    {
        private readonly IRepositoryBase<Vaccination,VaccinationFilter> _vaccinationRepository;

        public VaccinationService(IRepositoryBase<Vaccination,VaccinationFilter> vaccinationRepository)
        {
            _vaccinationRepository = vaccinationRepository;
        }

        public async Task<IEnumerable<Vaccination>> GetAllAsync(VaccinationFilter filter)
        {
            return await _vaccinationRepository.GetAllAsync(filter);
        }

        public async Task<Vaccination> GetByIdAsync(int id)
        {
            return await _vaccinationRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Vaccination vaccination)
        {
            await _vaccinationRepository.CreateAsync(vaccination);
        }

        public async Task UpdateAsync(Vaccination vaccination)
        {
            await _vaccinationRepository.UpdateAsync(vaccination);
        }

        public async Task DeleteAsync(int id)
        {
            await _vaccinationRepository.DeleteAsync(id);
        }
    }
}
