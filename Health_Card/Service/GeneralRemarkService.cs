using System.Collections.Generic;
using System.Threading.Tasks;
using Health_Card.Interface.GeneralRemark;
using Health_Card.Model;

namespace Health_Card.Service
{
    public class GeneralRemarkService : IGeneralRemarkService
    {
        private readonly IGeneralRemarkRepository _generalRemarkRepository;

        public GeneralRemarkService(IGeneralRemarkRepository generalRemarkRepository)
        {
            _generalRemarkRepository = generalRemarkRepository;
        }

        public async Task<IEnumerable<GeneralRemark>> GetAllAsync()
        {
            return await _generalRemarkRepository.GetAllAsync();
        }

        public async Task<GeneralRemark> GetByIdAsync(int id)
        {
            return await _generalRemarkRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(GeneralRemark generalRemark)
        {
            await _generalRemarkRepository.CreateAsync(generalRemark);
        }

        public async Task UpdateAsync(GeneralRemark generalRemark)
        {
            await _generalRemarkRepository.UpdateAsync(generalRemark);
        }

        public async Task DeleteAsync(int id)
        {
            await _generalRemarkRepository.DeleteAsync(id);
        }
    }
}
