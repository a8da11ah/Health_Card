using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Dto;
using Health_Card.Interface;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class ChronicDiseaseService : IServiceBase<ChronicDisease,ChronicDiseaseFilter>
    {
        private readonly IServiceBase<ChronicDisease,ChronicDiseaseFilter> _chronicDiseaseRepository;

        public ChronicDiseaseService(IServiceBase<ChronicDisease,ChronicDiseaseFilter> chronicDiseaseRepository)
        {
            _chronicDiseaseRepository = chronicDiseaseRepository;
        }

        public async Task<IEnumerable<ChronicDisease>> GetAllAsync(ChronicDiseaseFilter filter)
        {
            return await _chronicDiseaseRepository.GetAllAsync(filter);
        }

        public async Task<ChronicDisease> GetByIdAsync(int id)
        {
            return await _chronicDiseaseRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(ChronicDisease chronicDisease)
        {
            await _chronicDiseaseRepository.CreateAsync(chronicDisease);
        }

        public async Task UpdateAsync(ChronicDisease chronicDisease)
        {
            await _chronicDiseaseRepository.UpdateAsync(chronicDisease);
        }

        public async Task DeleteAsync(int id)
        {
            await _chronicDiseaseRepository.DeleteAsync(id);
        }
    }
}
