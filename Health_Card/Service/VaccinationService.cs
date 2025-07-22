using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface.Vaccination;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class VaccinationService : IVaccinationService
    {
        private readonly IVaccinationRepository _vaccinationRepository;

        public VaccinationService(IVaccinationRepository vaccinationRepository)
        {
            _vaccinationRepository = vaccinationRepository;
        }

        public async Task<IEnumerable<Vaccination>> GetAllAsync()
        {
            return await _vaccinationRepository.GetAllAsync();
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
